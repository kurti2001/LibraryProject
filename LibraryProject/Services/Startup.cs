namespace LibraryProject.Services
{
    public static class Startup
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IOrderItemsService, OrderItemsService>();
            services.AddScoped<IServicesManager, ServicesManager>();


            services.AddSingleton<IBooksCartService, BooksCartService>();
        }
    }
}
