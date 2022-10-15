using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoackApp.Core.Application.Interfaces.Services;
using StockApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseFirstExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _service;
        public HomeController(IProductService productService)
        {
            _service = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllViewModel());
        }          
    }
}
