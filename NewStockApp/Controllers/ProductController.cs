using Application.Services;
using Microsoft.AspNetCore.Mvc;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Products;
using WebApp.NewStockApp.Middlewares;

namespace DatabaseFirstExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _Categoryservice;
        private readonly ValidateUserSession _validateUserSession;

        public ProductController(IProductService productService, ICategoryService categoryservice, ValidateUserSession validateUserSession)
        {
            _service = productService;
            _Categoryservice = categoryservice;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _service.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveProductViewModel vm = new();
            vm.Categories = await _Categoryservice.GetAllViewModel(); 

            return View("SaveProduct", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                vm.Categories = await _Categoryservice.GetAllViewModel();
                return View("SaveProduct", vm);
            }

            SaveProductViewModel productVm = await _service.Add(vm);
            if (productVm != null && productVm.Id != 0)
            {
                productVm.ImagePath = UploadFile(vm.File, productVm.Id);
                await _service.Update(productVm);
            }
            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveProductViewModel vm = await _service.GetByIdSaveViewModel(id);
            vm.Categories = await _Categoryservice.GetAllViewModel();

            return View("SaveProduct", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProductViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

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
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _service.Delete(id);

            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id)
        {
            //get directory
            string basePath = $"/Images/Products/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //Create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file path
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string filenameWithPath = Path.Combine(path, filename);

            using(var stream = new FileStream(filenameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine(basePath, filename);
        }
    }
}
