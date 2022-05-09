using System;
using System.Collections.Generic;
using System.Linq;
using Office.Infrastructure.BasicAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Office.Context.Models;
using Office.Services;
using Office.Context.Dtos;
using Office.Services.Interfaces;

namespace Office.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> _logger;
        private readonly ITagService _tagService;


        public TagController(ILogger<TagController> logger, ITagService tagService )
        {
            _logger = logger;
            _tagService = tagService;
        }



        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("FilterTagByStatus")]
        [Authorize(Roles = "Admin")]
        public List<Tag> FilterTagById(int statusId)
        {
            try
            {
               var filteredTags= _tagService.FilteredTagsById(statusId);
                return filteredTags;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("FilterTagByStatusName")]
        [Authorize(Roles = "Admin")]
        public List<Tag> FilterTagByStatusName(string statusName)
        {
            try
            {
                var filteredTags = _tagService.FilteredTagsByName(statusName);
                return filteredTags;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("DeactivateTagForUser")]
        [Authorize(Roles ="Admin")]
        public string DeactivateTagForUser(int userId)
        {
            try
            {
                var filteredTags = _tagService.DeactivateTagForUser(userId);
                if (filteredTags)
                    return "Tag deactivated succesfully!";
                else
                    return "Could not deactivate Tag. Something happend!";
            }
            catch (Exception ex)
            {
                return "Could not deactivate Tag. Something happend! Error: " + ex.Message;
            }
        }
        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("CreateTagForUser")]
        [Authorize(Roles = "Admin")]
        public Tag CreateTagForUser([FromBody] CreateTagForUserModel createTagForUserModel)
        {
            try
            {
                var filteredTags = _tagService.AddTag(createTagForUserModel);
                return filteredTags;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("ActivateTagForUser")]
        [Authorize(Roles = "Admin")]
        public string ActivateTagForUser(int userId)
        {
            try
            {
                var filteredTags = _tagService.ActivateTagForUser(userId);
                if (filteredTags)
                    return "Tag deactivated succesfully!";
                else
                    return "Could not deactivate Tag. Something happend!";
            }
            catch (Exception ex)
            {
                return "Could not deactivate Tag. Something happend! Error: " + ex.Message;
            }
        }

    }
}
