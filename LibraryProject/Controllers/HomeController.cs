using LibraryProject.DataAccess.Models;
using LibraryProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriesService _categoriesService;
        private readonly IBooksService _booksService;

        public HomeController(ILogger<HomeController> logger,
                              ICategoriesService categoriesService,
                              IBooksService booksService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _booksService = booksService;
        }

        public async Task<IActionResult> GetBooksByCategory(int id)
        {
            var books = await _booksService.GetBooksByCategory(id);
            return View("BooksByCategory", books);
        }

        public async Task< IActionResult> Index()
        {
            var categories = await _categoriesService.GetAllAsync();
            var sortedCategories = categories.OrderBy(c => c.Name).ToList();
            ViewBag.Categories = categories;

            var books = await _booksService.GetAllAsync();
            var randomBooks = books.OrderBy(x => Guid.NewGuid()).ToList();
            var booksToShow = randomBooks.Take(9);
            return View(booksToShow); 
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
