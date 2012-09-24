using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DynamicLoopIoC.Models.Attributes;

namespace DynamicLoopIoC.Models.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [ISBN]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        public List<AuthorListItemModel> Authors { get; set; }

        public bool IsEditMode { get; set; }
    }
}
