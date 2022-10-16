using Microsoft.AspNetCore.Mvc;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Categories;


namespace DatabaseFirstExample.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService categoryService)
        {
            _service = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllViewModel();
            return View(list);
        }

        public IActionResult Create()
        {
            return View("SaveCategory", new SaveCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _service.Add(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveCategory", await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _service.Update(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {              
            await _service.Delete(id);

            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }
    }
}
