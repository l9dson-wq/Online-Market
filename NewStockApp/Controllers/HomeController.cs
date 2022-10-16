using Microsoft.AspNetCore.Mvc;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Products;

namespace DatabaseFirstExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _Categoryservice;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _service = productService;
            _Categoryservice = categoryService;
        }

        public async Task<IActionResult> Index(FilterProductViewModel vm )
        {
            ViewBag.Categories = await _Categoryservice.GetAllViewModel(); 
            return View(await _service.GetAllViewModelWithFilters(vm));
        }          
    }
}
