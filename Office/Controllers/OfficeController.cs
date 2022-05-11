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
    public class OfficeController : ControllerBase
    {
        private readonly ILogger<OfficeController> _logger;
        private readonly IOfficeService _OfficeService;


        public OfficeController(ILogger<OfficeController> logger, IOfficeService officeService)
        {
            _logger = logger;
            _OfficeService = officeService;
        }



        /// <summary>
        /// API requires JWT auth
        /// </summary>
        /// <returns></returns>
        [HttpPost("EnterTheOffice")]
        [Authorize(Roles = "Admin, Employee, Guest")]
        public IActionResult EnterTheOffice([FromBody] OfficeEnterAndLeaveRequest officeEnterAndLeaveRequest)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Invalid request");

                }

                var filteredTags= _OfficeService.EnterOrLeaveTheOffice(officeEnterAndLeaveRequest,true);
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
        [HttpPost("LeaveTheOffice")]
        [Authorize(Roles = "Admin, Enmloyee, Guest")]
        public IActionResult LeaveTheOffice([FromBody] OfficeEnterAndLeaveRequest officeEnterAndLeaveRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(Convert.ToUInt16(HttpStatusCode.BadRequest), "Invalid request");

                }
                var filteredTags = _OfficeService.EnterOrLeaveTheOffice(officeEnterAndLeaveRequest, false);
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
    }
}
