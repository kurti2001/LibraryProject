﻿using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
