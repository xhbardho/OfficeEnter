using Office.Context.Dtos;
using Office.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Services.Interfaces
{
    public interface ITagService
    {
       List<Tag> FilteredTagsByName(string statusName);
       List<Tag> FilteredTagsById(int statusId);
       bool DeactivateTagForUser(int userId);
       Tag AddTag(CreateTagForUserModel tag);
       int GetTagStatusIdByStatusName(string statusName);

    }
}
