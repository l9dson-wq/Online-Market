using Application.Services;
using Microsoft.AspNetCore.Mvc;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Products;
using StockApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseFirstExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService productService)
        {
            _service = productService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllViewModel();
            return View(list);
        }

        public IActionResult Create()
        {
            return View("SaveProduct", new SaveProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProduct", vm);
            }

            await _service.Add(vm);
            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveProduct", await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProduct", vm);
            }

            await _service.Update(vm);
            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {              
            await _service.Delete(id);

            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }
    }
}
