using System.ComponentModel;

namespace DynamicLoopIoC.Models.Models
{
    public enum AuthorsListSuccessMessage
    {
        [Description("None")]
        None = 0,
        [Description("Author added succesfully")]
        AuthorAddedSuccesfully = 1,
        [Description("Author edited succesfully")]
        AuthorEditedSuccesfully = 2,
        [Description("Author deleted succesfully")]
        AuthorDeletedSuccesfully = 3,
        [Description("Author not deleted as linked to a book")]
        AuthorNotDeletedSuccesfully = 4
    }
}