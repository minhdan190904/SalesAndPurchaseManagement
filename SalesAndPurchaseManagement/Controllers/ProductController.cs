﻿using Microsoft.AspNetCore.Mvc;

namespace SalesAndPurchaseManagement.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
