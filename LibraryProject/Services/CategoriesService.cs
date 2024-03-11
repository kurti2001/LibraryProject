using LibraryProject.DataAccess;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(int id, Category model);
        Task<Category> DeleteAsync(int id);
    }
    public class CategoriesService : ICategoriesService
    {
        private readonly LibraryProjectDbContext _context;
        public CategoriesService(LibraryProjectDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if(category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
                return category;
            }
            throw new Exception();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(int id, Category model)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                category.Name = model.Name;
                await _context.SaveChangesAsync();
                return category;
            }
            throw new Exception();
        }
    }
}
