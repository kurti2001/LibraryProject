using LibraryProject.DataAccess.Models;

namespace LibraryProject.Services
{
    public interface IBooksCartService
    {
        void AddToCart(int userId, BookCart item);
        IEnumerable<BookCart> GetAll(int userId);
        int GetNoItems(int userId);
        void RemoveFromCart(int userId, int bookId);
    }
    internal class BooksCartService : IBooksCartService
    {
        private readonly Dictionary<int, List<BookCart>>_bookCart;
        public BooksCartService()
        {
            _bookCart = new Dictionary<int, List<BookCart>>();
        }
        public void AddToCart(int userId, BookCart item)
        {
            if(!_bookCart.ContainsKey(userId))
            {
                _bookCart.Add(userId, new List<BookCart>());
            }
            var addedBooksItem = _bookCart[userId].FirstOrDefault(x => x.BookId == item.BookId);
            if(addedBooksItem != null)
            {
                return;
            }
            _bookCart[userId].Add(item);
        }

        public IEnumerable<BookCart> GetAll(int userId)
        {
            if (_bookCart.ContainsKey(userId))
            {
                return _bookCart[userId];
            }
            else
            {
                return Enumerable.Empty<BookCart>();
            }
        }

        public int GetNoItems(int userId)
        {
           if(_bookCart.ContainsKey(userId))
            {
                return _bookCart[userId].Count;
            }
            return 0;
        }
        public void RemoveFromCart(int userId, int bookId)
        {
            if (_bookCart.ContainsKey(userId))
            {
                var userCart = _bookCart[userId];
                var bookToRemove = userCart.FirstOrDefault(x => x.BookId == bookId);
                if (bookToRemove != null)
                {
                    userCart.Remove(bookToRemove);
                }
            }
        }

    }
}
    