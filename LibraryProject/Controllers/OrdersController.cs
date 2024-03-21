using LibraryProject.DataAccess.Models;
using LibraryProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LibraryProject.Controllers
{
    
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IOrderItemsService _orderItemsService;
        private readonly IBooksService _booksService;
        private readonly UserManager<User> _userManager;

        public OrdersController(IOrdersService ordersService, 
                                IOrderItemsService orderItemsService,
                                IBooksService booksService, 
                                UserManager<User> userManager)
        {
            _ordersService = ordersService;
            _orderItemsService = orderItemsService;
            _booksService = booksService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            int authorizedUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = _ordersService.GetUserOrders(authorizedUserId);
            return View(orders);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var order = _ordersService.GetById(id.Value);
            int authorizedUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (order.UserDTO.Id == authorizedUserId)
            {
                order.Items = _orderItemsService.GetByOrderId(order.Id);
                var books = _booksService.GetRange(order.Items.Select(x => x.Book.Id).ToList());
                foreach( var oItem in order.Items)
                {
                    oItem.Book = books.FirstOrDefault(x => x.Id == oItem.Book.Id);
                }
                var user = _userManager.Users
                                       .AsNoTracking()
                                       .FirstOrDefault(x => x.Id == order.UserDTO.Id);
                order.UserDTO = new DTO.UserDTO
                {
                    Email = user.Email,
                    LastName = user.UserName,
                    Name = user.UserName
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
