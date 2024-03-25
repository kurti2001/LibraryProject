namespace LibraryProject.DataAccess.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; internal set; }
    }
}
