using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryProject.DataAccess
{
	public class LibraryProjectDbContext : DbContext
	{
		public LibraryProjectDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder) 
		{ 
		}
		public DbSet<Category> Category { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public DbSet<Book> Book { get; set; } = default!;
	}
}
