using LibraryProject.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryProject.DataAccess
{
	public static class Startup
	{
		public static void RegisterDataAccessServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<LibraryProjectDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LibraryProjectConnectionString")));

            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
           .AddDefaultUI()
           .AddDefaultTokenProviders()
           .AddEntityFrameworkStores<LibraryProjectDbContext>();
        }
	}
}
