namespace LibraryProject.Services
{
    public static class Startup
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesService, CategoriesService>();
        }
    }
}
