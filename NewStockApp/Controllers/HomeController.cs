using Microsoft.AspNetCore.Mvc;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Products;
using WebApp.NewStockApp.Middlewares;

namespace DatabaseFirstExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _Categoryservice;
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(IProductService productService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _service = productService;
            _Categoryservice = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(FilterProductViewModel vm )
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Categories = await _Categoryservice.GetAllViewModel(); 
            return View(await _service.GetAllViewModelWithFilters(vm));
        }          
    }
}
