using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Office.Context;
using Office.Context.Dtos;
using Office.Helper;
using Office.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly ILogger<OfficeService> _logger;
        private readonly OfficeDbContext _officeDbContext;
        public OfficeService(ILogger<OfficeService> logger, OfficeDbContext officeDbContext)
        {
            _logger = logger;
            _officeDbContext = officeDbContext;
        }
        public OfficeEnterAndLeaveResponse EnterOrLeaveTheOffice(OfficeEnterAndLeaveRequest officeEnterAndLeaveRequest, bool isEnterType)
        {
            OfficeEnterAndLeaveResponse officeEnterAndLeaveResponse = new OfficeEnterAndLeaveResponse();
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.Id == officeEnterAndLeaveRequest.TagId).FirstOrDefault();
                if (tag == null)
                {
                    officeEnterAndLeaveResponse.isSuccesfull = false;
                    officeEnterAndLeaveResponse.Message = "This tag does not exists!!";
                    return officeEnterAndLeaveResponse;

                }
                else 
                {
                    if (tag.TagStatus.Description==StaticStrings.PENDING_TAG_DESCRIPTION)
                    {
                        officeEnterAndLeaveResponse.isSuccesfull = false;
                        officeEnterAndLeaveResponse.Message = "This tag is not activated yet!!";
                        return officeEnterAndLeaveResponse;

                    }
                    if (tag.TagStatus.Description == StaticStrings.DEACTIVATE_TAG_DESCRIPTION)
                    {
                        officeEnterAndLeaveResponse.isSuccesfull = false;
                        officeEnterAndLeaveResponse.Message = "This tag  is deactivated!!";
                        return officeEnterAndLeaveResponse;

                    }
                    if (HasTagExpired(officeEnterAndLeaveRequest.TagId)) 
                    {
                        officeEnterAndLeaveResponse.isSuccesfull = false;
                        officeEnterAndLeaveResponse.Message = "This tag  has expired!!";
                        return officeEnterAndLeaveResponse;

                    }
                    var isAlreadyInOrOut = IsAlreadyInOrOut(officeEnterAndLeaveRequest.TagId, isEnterType);
                    if (isAlreadyInOrOut.isAlreadyInOrOut) 
                    {
                        officeEnterAndLeaveResponse.isSuccesfull = false;
                        officeEnterAndLeaveResponse.Message = isAlreadyInOrOut.InOrOut;
                        return officeEnterAndLeaveResponse;
                    }
                    officeEnterAndLeaveResponse.isSuccesfull = true;
                    officeEnterAndLeaveResponse.Message = "You were succesfully checked in!!";
                    return officeEnterAndLeaveResponse;

                }


            }
            catch (Exception ex)
            {

                throw;
            }        
        }

        public bool HasTagExpired(int id)
        {
            try
            {
                var tag = _officeDbContext.Tags.Where(x => x.Id == id).FirstOrDefault();
                var time = tag != null ? tag.ExpiredTime : DateTime.Parse("0000-00-00");
                if (DateTime.Now < time) 
                {
                    tag.IsValid = 0;
                    tag.TagStatusId = GetTagStatusIdByStatusName(StaticStrings.EXPIRE_TAG_DESCRIPTION);
                    _officeDbContext.SaveChanges();
                    return false;
                }
                return true; 
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IsAlreadyInOrOut IsAlreadyInOrOut(int tagid,bool isEnterType)
        {
            IsAlreadyInOrOut modelToReturn = new IsAlreadyInOrOut();
            try
            {
                var officeEnterAndLeaves = _officeDbContext.officeEnterAndLeaves.Where(x => x.TagId == tagid).OrderByDescending(x=>x.Time).FirstOrDefault();
                if (officeEnterAndLeaves != null) 
                {
                    if (officeEnterAndLeaves.IsTypeEnter == isEnterType) 
                    {
                        modelToReturn.isAlreadyInOrOut = true;
                        modelToReturn.InOrOut = officeEnterAndLeaves.IsTypeEnter == true ? "You are already in!" : "You are already out!";
                    }
                    modelToReturn.isAlreadyInOrOut = false;
                }
                return modelToReturn;
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
                id = tag != null ? tag.Id : 0;
                return id;
            }
            catch (Exception)
            {
                id = -1;
                return id;
            }

        }
    }
}
