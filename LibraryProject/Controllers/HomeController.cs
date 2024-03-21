using LibraryProject.DataAccess.Models;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoriesService _categoriesService;
    private readonly IBooksService _booksService;
    private readonly IOrdersService _ordersService;

    public HomeController(ILogger<HomeController> logger,
                          ICategoriesService categoriesService,
                          IBooksService booksService,
                          IOrdersService ordersService)
    {
        _logger = logger;
        _categoriesService = categoriesService;
        _booksService = booksService;
        _ordersService = ordersService;
    }

    public async Task<IActionResult> BooksByCategory(int categoryId)
    {
        var category = await _categoriesService.GetByIdAsync(categoryId);
        if (category == null)
        {
            return NotFound();
        }

        var booksInCategory = await _booksService.GetBooksByCategory(categoryId);
        ViewBag.CategoryName = category.Name;
        return View(booksInCategory);
    }

    [HttpGet]
    public async Task<IActionResult> SearchBooksByTitle(string title)
    {
        var books = await _booksService.GetAllAsync();

        if (!String.IsNullOrEmpty(title))
        {
            books = books.Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        return View("BooksByCategory", books);
    }

    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Admin") || User.IsInRole("Recepsionist"))
        {
            var books = await _booksService.GetAllAsync();
            var randomBooks = books.OrderBy(x => Guid.NewGuid()).ToList();
            return View("AdmIndex", randomBooks);
        }
        else
        {
            if (User.IsInRole("User"))
            {
                int authorizedUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var orders = _ordersService.GetUserOrders(authorizedUserId);
                ViewBag.Orders = orders;

            }
                var categories = await _categoriesService.GetAllAsync();
            var sortedCategories = categories.OrderBy(c => c.Name).ToList();
            ViewBag.Categories = sortedCategories;

            var books = await _booksService.GetAllAsync();
            var randomBooks = books.OrderBy(x => Guid.NewGuid()).ToList();
            var booksToShow = randomBooks.Take(9);
            return View(booksToShow);
        }
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
