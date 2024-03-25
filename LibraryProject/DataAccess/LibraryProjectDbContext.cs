using LibraryProject.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryProject.DataAccess
{
	public class LibraryProjectDbContext : IdentityDbContext<User,Role, int>
	{
		public LibraryProjectDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder) 
		{ 
		}
		public DbSet<Category> Category { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
		public DbSet<Book> Book { get; set; }
		public DbSet<CartItems> CartItems { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		
	}
}
