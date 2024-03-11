
using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(int id,Book model);
        Task<Book> DeleteAsync(int id);
    }
    public class BooksService : IBooksService
    {
        private readonly LibraryProjectDbContext _context;
        public BooksService(LibraryProjectDbContext context) 
        { 
            _context = context;
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            if(book != null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
                return book;
            }
            throw new Exception();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task<Book> UpdateAsync(int id, Book modle)
        {
            var book = await GetByIdAsync(id);
            if( book != null )
            {
                book.Title = book.Title;
                book.Description = book.Description;
                book.ISBN = book.ISBN;
                book.Created = book.Created;
                book.Updated = book.Updated;
                book.CategoryId = book.CategoryId;
                await _context.SaveChangesAsync();
                return book;
            }
            throw new Exception();
        }
    }
}
