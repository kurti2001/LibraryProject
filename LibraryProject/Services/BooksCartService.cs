using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;

namespace LibraryProject.Services
{
    public interface IBooksCartService
    {
        void AddToCart(int userId, BookCart item);
        IEnumerable<BookCart> GetAll(int userId);
        int GetNoItems(int userId);
        void RemoveFromCart(int userId, int bookId);
        void RemoveAllCartItems(int userId);
    }

    internal class BooksCartService : IBooksCartService
    {
        private readonly ICartItemsService _cartItemsService;
        private readonly Dictionary<int, List<BookCart>> _bookCart;
        private readonly LibraryProjectDbContext _context;

        public BooksCartService(ICartItemsService cartItemsService,
                                LibraryProjectDbContext context)
        {
            _cartItemsService = cartItemsService;
            _bookCart = new Dictionary<int, List<BookCart>>();
            _context = context;
        }

        public void AddToCart(int userId, BookCart item)
        {
            if (!_bookCart.ContainsKey(userId))
            {
                _bookCart.Add(userId, new List<BookCart>());
            }
            var addedBooksItem = _bookCart[userId].FirstOrDefault(x => x.BookId == item.BookId);
            if (addedBooksItem != null)
            {
                return;
            }
            _bookCart[userId].Add(item);

            _cartItemsService.AddCartItem(new CartItems { UserId = userId, BookId = item.BookId });
        }

        public IEnumerable<BookCart> GetAll(int userId)
        {
            var dbCartItems = _cartItemsService.GetCartItemsByUserId(userId);

            var cartItems = dbCartItems.Select(ci => new BookCart { BookId = ci.BookId }).ToList();
            if (_bookCart.ContainsKey(userId))
            {
                cartItems.AddRange(_bookCart[userId]);
            }

            return cartItems;
        }


        public int GetNoItems(int userId)
        {
            var dbCartItems = _cartItemsService.GetCartItemsByUserId(userId);
            var cartItemCount = dbCartItems.Count();

            if (_bookCart.ContainsKey(userId))
            {
                cartItemCount += _bookCart[userId].Count;
            }

            return cartItemCount;
        }

        public void RemoveFromCart(int userId, int bookId)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(ci => ci.UserId == userId && ci.BookId == bookId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }
        public void RemoveAllCartItems(int userId)
        {
            var cartItems = _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToList();

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

    }
}
