using Application.Services;
using Microsoft.AspNetCore.Mvc;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Users;

namespace DatabaseFirstExample.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        
        public UserController(IUserService productService)
        {
            _service = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            return View();
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                return View(userVm);
            }

            await _service.Add(userVm);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
