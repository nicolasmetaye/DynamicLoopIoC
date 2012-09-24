using System.ComponentModel.DataAnnotations;

namespace DynamicLoopIoC.Models.Models
{

    public class AuthorModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public bool IsEditMode { get; set; }
    }
}
