using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using LibraryProject.DTO;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public interface IOrderItemsService
    {
        void Add(BookCart bookCartModel, int orderId);
        List<OrderItemDTO> GetByOrderId(int id);
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

        public List<OrderItemDTO> GetByOrderId(int id)
        {
            return _context.OrderItem
                           .Where(x => x.OrderId == id)
                           .AsNoTracking()
                           .Select(x=> new OrderItemDTO
                           {
                               Book = new Book
                               { Id = x.Id}
                           }).ToList();
        }
    }
}
