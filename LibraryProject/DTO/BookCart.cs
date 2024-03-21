using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibraryProject.DataAccess.Models
{
    public class BookCart
    {
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
    public class BookCartModel
    {
        public int BookId { get; set; }
        public List<BookCart> Books { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
    }
}
