using Office.Services.Interfaces;
using Office.Context.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Office.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Office.Context.Dtos;
using Office.Helper;
using System.Net.Http;
using System.Net;

namespace Office.Services
{
    public class TagService : ITagService
    {
        private readonly ILogger<TagService> _logger;
        private readonly OfficeDbContext _officeDbContext;
        public TagService(ILogger<TagService> logger, OfficeDbContext officeDbContext)
        {
            _logger = logger;
            _officeDbContext = officeDbContext;
        }
        public Tag AddTag(CreateTagForUserModel tagViewModel)
        {
            try
            {
                if (CheckIfIsAlreadyATagForThisUser(tagViewModel.UserId)) 
                {
                    return null;
                }

                Tag tag = new Tag
                {
                    TagStatusId = tagViewModel.TagStatus,
                    UserId = tagViewModel.UserId,
                    Description = tagViewModel.TagName,
                    CreationTime = DateTime.Now
                };
                var user = USerById(tagViewModel.UserId);
                if (user==null)
                {
                    return null;
                }
                if (user.Role.Name == "Admin")
                    tag.ExpiredTime = DateTime.Now.AddYears(3);
                else if (user.Role.Name == "Employee")
                    tag.ExpiredTime = DateTime.Now.AddYears(1);
                else if (user.Role.Name == "Guest")
                    tag.ExpiredTime = DateTime.Now.AddHours(1);
                _officeDbContext.Add(tag);
                 _officeDbContext.SaveChanges();
                return tag;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public bool ActivateTagForUser(int userId)
        {
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.UserId == userId).ToList().FirstOrDefault();
                if (tag==null)
                {
                    return false;
                }
                int activateTagStatusId = GetTagStatusIdByStatusName(StaticStrings.ACTIVE_TAG_DESCRIPTION);
                tag.TagStatusId = activateTagStatusId;
                _officeDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Try again"),
                    ReasonPhrase = "Something wrong happend! Try again"
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public bool DeactivateTagForUser(int userId)
        {
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.UserId == userId).ToList().FirstOrDefault();
                if (tag==null)
                {
                    return false;
                }
                int deactivateTagStatusId = GetTagStatusIdByStatusName(StaticStrings.DEACTIVATE_TAG_DESCRIPTION);
                tag.TagStatusId = deactivateTagStatusId;
                 _officeDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public List<Tag> FilteredTagsById(int statusId)
        {
            try
            {
                var tags =  _officeDbContext.Tags.Where(x => x.TagStatus.Id == statusId).ToList();
                return tags;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }        
        }
        public List<Tag> FilteredTagsByName(string statusName)
        {
            try
            {
                var tags =  _officeDbContext.Tags.Where(x => x.TagStatus.Description == statusName).ToList();
                return tags;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public List<Tag> GetAllTags()
        {
            try
            {
                var tags = _officeDbContext.Tags.ToList();
                return tags;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public int GetTagStatusIdByStatusName(string statusName)
        {
            int id = 0;
            try
            {
                var tag = _officeDbContext.TagStatus.Where(x => x.Description == statusName).FirstOrDefault();
                id = tag!=null ? tag.Id : 0;
                return id;
            }
            catch (Exception)
            {
                id=-1;
                return id;
            }
        }
        public User USerById(int userId)
        {
            User user = new User();
            try
            {
                user = _officeDbContext.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == userId);
            }
            catch (Exception ex)
            {

                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
            return user;
        }
        public bool CheckIfIsAlreadyATagForThisUser(int userId)
        {
            try
            {
                var tags = _officeDbContext.Tags.Where(x=>x.UserId== userId).FirstOrDefault();
                if (tags == null)
                    return false;
                else return true;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }


    }
}
