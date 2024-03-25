using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryProject.Services
{
    public interface ICartItemsService
    {
        void AddCartItem(CartItems cartItem);
        IEnumerable<CartItems> GetCartItemsByUserId(int userId);
    }

    internal class CartItemsService : ICartItemsService
    {
        private readonly LibraryProjectDbContext _context;

        public CartItemsService(LibraryProjectDbContext context)
        {
            _context = context;
        }

        public void AddCartItem(CartItems cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public IEnumerable<CartItems> GetCartItemsByUserId(int userId)
        {
            return _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToList();
        }
    }
}
