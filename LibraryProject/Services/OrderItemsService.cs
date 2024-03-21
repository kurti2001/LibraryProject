using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public interface IOrderItemsService
    {
        void Add(BookCart bookCartModel, int orderId);
        IEnumerable<OrderItem> GetByOrderId(int orderId);
    }

    internal class OrderItemsService : IOrderItemsService
    {
        private readonly LibraryProjectDbContext _context;

        public OrderItemsService(LibraryProjectDbContext context)
        {
            _context = context;
        }

        public void Add(BookCart bookCartModel, int orderId)
        {
            _context.OrderItem.Add(new OrderItem
            {
                OrderId = orderId,
                BookId = bookCartModel.BookId
            });

            _context.SaveChanges();
        }

        public IEnumerable<OrderItem> GetByOrderId(int orderId)
        {
            return _context.OrderItem
                           .Where(x => x.OrderId == orderId)
                           .AsNoTracking()
                           .ToList();
        }
    }
}
