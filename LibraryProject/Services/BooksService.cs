using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task<Book> UpdateAsync(int id, Book model);
        Task<Book> DeleteAsync(int id);
        Task<IEnumerable<Book>> GetBooksByCategory(int id);
        Task<List<BookModel>> FindByTitle(string title);
    }
    internal class BooksService : IBooksService
    {
        private readonly LibraryProjectDbContext _context;
        public BooksService(LibraryProjectDbContext context) 
        { 
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
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

        public async Task<List<BookModel>> FindByTitle(string title)
        {
            var books = await _context.Book.Where(x => x.Title.ToLower().Contains(title.ToLower()) ||
                                                x.Description.ToLower().Contains(title.ToLower()) ||
                                                x.ISBN.ToLower().Contains(title.ToLower()) ||
                                                x.Created.ToString().ToLower().Contains(title.ToLower()) ||
                                                x.Updated.ToString().ToLower().Contains(title.ToLower()) ||
                                                x.CategoryId.ToString().Contains(title.ToLower()))
                                    .ToListAsync();

            return books.Select(x => new BookModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ISBN = x.ISBN,
                Created = x.Created,
                Updated = x.Updated,
                CategoryId = x.CategoryId
            }).ToList();
        }
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(int id)
        {
            var books = await _context.Book
                                      .Where(b => b.CategoryId == id)
                                      .ToListAsync();
            return books;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task<Book> UpdateAsync(int id, Book model)
        {
            var book = await GetByIdAsync(id);
            if( book != null )
            {
                book.Title = model.Title;
                book.Description = model.Description;
                book.ISBN = model.ISBN;
                book.Created = model.Created;
                book.Updated = model.Updated;
                book.CategoryId =   book.CategoryId;
                await _context.SaveChangesAsync();
                return book;
            }
            throw new Exception();
        }
    }
}
