using System.ComponentModel;

namespace DynamicLoopIoC.Models.Models
{
    public enum BooksListSuccessMessage
    {
        [Description("None")]
        None = 0,
        [Description("Book added succesfully")]
        BookAddedSuccesfully = 1,
        [Description("Book edited succesfully")]
        BookEditedSuccesfully = 2,
        [Description("Book deleted succesfully")]
        BookDeletedSuccesfully = 3
    }
}