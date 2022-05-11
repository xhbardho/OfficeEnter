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
using System.Net.Http;
using System.Net;

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
        public IActionResult FilterTagById(int statusId)
        {
            try
            {
                if (statusId <= 0)
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Invalid ID");

                }
                var filteredTags= _tagService.FilteredTagsById(statusId);
               return  Ok(filteredTags);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Something wrong happend. {ex.Message}"),
                    ReasonPhrase = $"Something wrong happend. {ex.Message}."
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("FilterTagByStatusName")]
        [Authorize(Roles = "Admin")]
        public IActionResult FilterTagByStatusName(string statusName)
        {
            try
            {
                if (string.IsNullOrEmpty(statusName)) 
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Invalid status");

                }
                var filteredTags = _tagService.FilteredTagsByName(statusName);
                return Ok(filteredTags);
            }
            catch (Exception ex)
            {


                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Something wrong happend. {ex.Message}"),
                    ReasonPhrase = $"Something wrong happend. {ex.Message}."
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }

        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeactivateTagForUser")]
        [Authorize(Roles ="Admin")]
        public IActionResult DeactivateTagForUser(int userId)
        {
            try
            {
                if (userId <= 0) 
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Invalid ID");
                }

                var filteredTags = _tagService.DeactivateTagForUser(userId);
                if (filteredTags)
                    return Ok("Tag deactivated succesfully!");
                else
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Could not deactivate Tag. Something wrong happend!");

                }
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Could not deactivate Tag. Something happend! Error: " + ex.Message),
                    ReasonPhrase = "Could not deactivate Tag. Something happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateTagForUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTagForUser([FromBody] CreateTagForUserModel createTagForUserModel)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Invalid request");
                }
                var filteredTags = _tagService.AddTag(createTagForUserModel);
                if (filteredTags == null) 
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.NotFound), "User not found");

                }
                TagViewModel model = new TagViewModel(filteredTags);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Something wrong happend. {ex.Message}"),
                    ReasonPhrase = $"Something wrong happend. {ex.Message}."
                };
                throw new System.Web.Http.HttpResponseException(response);
               
            }
        }

        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpPost("ActivateTagForUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult ActivateTagForUser(int userId)
        {
            try
            {
                if (userId < 0) 
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Invalid request");

                }
                var filteredTags = _tagService.ActivateTagForUser(userId);
                if (filteredTags)
                    return Ok("Tag activated succesfully!");
                else
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Could not deactivate Tag. Something wrong happend!");

                }
            }
            catch (Exception)
            {

                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Something wrong happend. Try again later"),
                    ReasonPhrase = $"Something wrong happend. Try again later."
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }

    }
}
