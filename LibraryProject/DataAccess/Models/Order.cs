namespace LibraryProject.DataAccess.Entities
{
		public enum OrderStatus
		{
			CREATED = 1,
			DELIVERING = 2,
			COMPLETED = 3,
			RETURNED = 4
		}

		public class Order
		{
			public int Id { get; set; }
			public OrderStatus Status { get; set; }
			public decimal TotalPrice { get; set; }
			public string Address { get; set; }
			public string MobilePhone { get; set; }
			public DateTime CreatedOn { get; set; }
			public DateTime LastUpdatedOn { get; set; }
			//public int UserId { get; set; }
		}

		public class OrderItem
		{
			public int Id { get; set; }
			public int BookId { get; set; }
			public decimal Quantity { get; set; }
			public decimal Price { get; set; }
			public int OrderId { get; set; }
		}
	
}
