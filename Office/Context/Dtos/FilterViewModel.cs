using Office.Context.Models;
using System;

namespace Office.Context.Dtos
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int IsValid { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TagStatusId { get; set; }
        public TagStatus TagStatus { get; set; }

        public TagViewModel(Tag tag)
        {
            Id = tag.Id;
            Description=tag.Description;
            CreationTime=tag.CreationTime;
            ExpiredTime=tag.ExpiredTime;
            IsValid = tag.IsValid;
            UserId = tag.UserId;
            TagStatusId= tag.TagStatusId;
            TagStatus = tag.TagStatus;
        }
    }
}
