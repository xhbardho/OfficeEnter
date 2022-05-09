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

namespace Office.Services
{
    public class TagService : ITagService
    {
        private readonly ILogger<UserService> _logger;
        private readonly OfficeDbContext _officeDbContext;
        public TagService(ILogger<UserService> logger, OfficeDbContext officeDbContext)
        {
            _logger = logger;
            _officeDbContext = officeDbContext;
        }
        public Tag AddTag(CreateTagForUserModel tagViewModel)
        {
            try
            { Tag tag = new Tag();
                 _officeDbContext.Add(tag);
                 _officeDbContext.SaveChanges();
                return tag;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool DeactivateTagForUser(int userId)
        {
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.UserId == userId).ToList().FirstOrDefault();
                tag.TagStatusId = 4;
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
    }
}
