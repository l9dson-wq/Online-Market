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
        private readonly ICategoryService _Categoryservice;

        public ProductController(IProductService productService, ICategoryService categoryservice)
        {
            _service = productService;
            _Categoryservice = categoryservice;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllViewModel();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            SaveProductViewModel vm = new();
            vm.Categories = await _Categoryservice.GetAllViewModel(); 

            return View("SaveProduct", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _Categoryservice.GetAllViewModel();
                return View("SaveProduct", vm);
            }

            await _service.Add(vm);
            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveProductViewModel vm = await _service.GetByIdSaveViewModel(id);
            vm.Categories = await _Categoryservice.GetAllViewModel();

            return View("SaveProduct", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _Categoryservice.GetAllViewModel();
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
