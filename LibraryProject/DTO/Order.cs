using LibraryProject.DataAccess.Models;

namespace LibraryProject.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime Deadline { get; set; }
        public UserDTO UserDTO { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();

    }
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class OrderItemDTO
    {
        public Book Book { get; set; }
    }
}
