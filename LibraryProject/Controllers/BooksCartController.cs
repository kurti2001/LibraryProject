using LibraryProject.DataAccess.Models;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryProject.Controllers
{
    public class BooksCartController : Controller
    {
        private readonly IBooksCartService _booksCartService;
        private readonly IBooksService _booksService;
        private readonly IOrdersService _ordersService;
        private readonly IOrderItemsService _orderItemsSerice;
        private readonly IServicesManager _servicesManager;
        public BooksCartController(IBooksService booksService,
                                   IBooksCartService booksCartService,
                                   IOrdersService ordersSerivce,
                                   IOrderItemsService orderItemsService,
                                   IServicesManager servicesManager
                                   )
        {
            _booksCartService = booksCartService;
            _booksService = booksService;
            _ordersService = ordersSerivce;
            _servicesManager = servicesManager;
            _orderItemsSerice = orderItemsService;
        }
        private List<BookCart> getUserSelectedItems()
        {
            var userSelections =_booksCartService.GetAll(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            var booksIds = userSelections.Select(x => x.BookId)
                                          .Distinct()
                                          .ToList();
            var books = _booksService.GetRange(booksIds);
            foreach(var item in userSelections)
            {
                item.Book = books.FirstOrDefault(x=> x.Id == item.BookId);
            }
            return userSelections.ToList();
        }
      
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View(new BookCartModel
            {
                Books = getUserSelectedItems()
            });
        }
        [HttpPost]
        public IActionResult Create(BookCartModel model)
        {
            var orderAdded = _servicesManager.WrapInTransaction(() =>
            {
                var orderId = _ordersService.Add(model, int.Parse(User.FindFirstValue
            (ClaimTypes.NameIdentifier)));
                model.Books = getUserSelectedItems();
                foreach (var bookCartModel in model.Books)
                {
                    _orderItemsSerice.Add(bookCartModel, orderId);
                }
                return true;
            });
            return RedirectToAction("Index");
        }
    }
}
