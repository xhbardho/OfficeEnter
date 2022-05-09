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
        [HttpGet("EnterTheOffice")]
        [Authorize(Roles = "Admin, Enployee, Guest")]
        public OfficeEnterAndLeaveResponse EnterTheOffice([FromBody] OfficeEnterAndLeaveRequest officeEnterAndLeaveRequest)
        {
            try
            {
               var filteredTags= _OfficeService.EnterOrLeaveTheOffice(officeEnterAndLeaveRequest,true);
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
        [HttpGet("LeaveTheOffice")]
        [Authorize(Roles = "Admin, Enployee, Guest")]
        public OfficeEnterAndLeaveResponse LeaveTheOffice([FromBody] OfficeEnterAndLeaveRequest officeEnterAndLeaveRequest)
        {
            try
            {
                var filteredTags = _OfficeService.EnterOrLeaveTheOffice(officeEnterAndLeaveRequest, false);
                return filteredTags;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
