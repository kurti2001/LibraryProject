﻿using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using LibraryProject.DTO;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public interface IOrdersService
    {
        int Add(BookCartModel model, int userId);
        Order GetOrder(int id);
        IEnumerable<Order> GetUserOrders(int userId);
        OrderDTO GetById(int id);
    }
    internal class OrdersService : IOrdersService
    {
        private readonly LibraryProjectDbContext _context;

        public OrdersService(LibraryProjectDbContext context)
        {
            _context = context;
        }

        public int Add(BookCartModel model, int userId)
        {
            var order = new Order
            {
                Address = model.Address,
                CreatedOn = DateTime.UtcNow,
                Deadline = DateTime.UtcNow.AddDays(7),
                MobilePhone = model.MobilePhone,
                Status = OrderStatus.CREATED,
                UserId = userId
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public Order GetOrder(int id)
        {
            return _context.Order.Find(id) ?? throw new Exception("Order not found");
        }

        public OrderDTO GetById(int id)
        {
            var dbOrder = _context.Order.Find(id);
            return new OrderDTO
            {
                Id = dbOrder.Id,
                CreatedOn = dbOrder.CreatedOn,
                Deadline = dbOrder.Deadline,
                Address = dbOrder.Address,
                MobilePhone = dbOrder.MobilePhone,
                UserDTO = new UserDTO
                {
                    Id = dbOrder.UserId
                }
            };
        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            return _context.Order.Where(x => x.UserId == userId)
                                 .OrderByDescending(x => x.CreatedOn)
                                 .AsNoTracking()
                                 .ToList();
        }

    }
}
