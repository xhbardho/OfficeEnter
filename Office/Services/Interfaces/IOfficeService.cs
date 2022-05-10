using Office.Context.Dtos;
using Office.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Services.Interfaces
{
    public interface IOfficeService
    {
        public OfficeEnterAndLeaveResponse EnterOrLeaveTheOffice(OfficeEnterAndLeaveRequest officeEnterAndLeaveRequest, bool isEnterType);
        public bool HasTagExpired(int id);
        public IsAlreadyInOrOut IsAlreadyInOrOut(int tagid,bool isEnterType);
        public int GetTagStatusIdByStatusName(string statusName);
    }
}
