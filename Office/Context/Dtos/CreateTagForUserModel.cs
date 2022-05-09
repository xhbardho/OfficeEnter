using System.ComponentModel.DataAnnotations;

namespace Office.Context.Dtos
{
    public class CreateTagForUserModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string TagName { get; set; }
        [Required]
        public int TagStatus { get; set; }
    }
}
