using LibraryProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryProject.DataAccess
{
	internal class LibraryProjectDbContext : DbContext
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
		public DbSet<LibraryProject.DataAccess.Entities.Book> Book { get; set; } = default!;
	}
}
