namespace LibraryProject.DataAccess.Models
{
    public enum OrderStatus
    {
        CREATED = 1,
        PROCESSING = 2,
        DELIVERING = 3,
        COMPLETED = 4,
        RETURNED = 5
    }

    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime Deadline { get; set; }
        public int UserId { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int OrderId { get; set; }
    }

}
