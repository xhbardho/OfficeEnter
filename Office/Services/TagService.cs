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
                Tag tag = new Tag
                {
                    TagStatusId = tagViewModel.TagStatus,
                    UserId = tagViewModel.UserId,
                    Description = tagViewModel.TagName,
                    CreationTime = DateTime.Now
                };
                var user = USerById(tagViewModel.UserId);
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
                throw;
            }
        }
        public bool ActivateTagForUser(int userId)
        {
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.UserId == userId).ToList().FirstOrDefault();
                int activateTagStatusId = GetTagStatusIdByStatusName(StaticStrings.ACTIVE_TAG_DESCRIPTION);
                tag.TagStatusId = activateTagStatusId;
                _officeDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeactivateTagForUser(int userId)
        {
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.UserId == userId).ToList().FirstOrDefault();
                int deactivateTagStatusId = GetTagStatusIdByStatusName(StaticStrings.DEACTIVATE_TAG_DESCRIPTION);
                tag.TagStatusId = deactivateTagStatusId;
                 _officeDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
                throw;
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
                throw;
            }
        }
        public int GetTagStatusIdByStatusName(string statusName)
        {
            int id = 0;
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.TagStatus.Description == statusName).FirstOrDefault();
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

                throw ;
            }
            return user;
        }
    }
}
