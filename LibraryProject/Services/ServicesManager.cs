using LibraryProject.DataAccess;

namespace LibraryProject.Services
{
    public interface IServicesManager
    {
        T WrapInTransaction<T>(Func<T> action);
    }
    internal class ServicesManager : IServicesManager
    {
        private readonly LibraryProjectDbContext _context;
        public ServicesManager(LibraryProjectDbContext context)
        {
            _context = context;
        }
        private static object _lock = new object();
        public T WrapInTransaction<T>(Func<T>action)
        {
            lock (_lock)
            {
                using var trans = _context.Database.BeginTransaction();
                T result;
                try
                {
                    result = action();
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
                return result;
            }
        }
    }
}
