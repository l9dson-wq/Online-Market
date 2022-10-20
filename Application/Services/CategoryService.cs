
using Microsoft.AspNetCore.Http;
using StoackApp.Core.Application.Helpers;
using StoackApp.Core.Application.Interfaces.Repositories;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Categories;
using StoackApp.Core.Application.ViewModels.Products;
using StoackApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;


namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public CategoryService(ICategoryRepository CategoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = CategoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<SaveCategoryViewModel> Add(SaveCategoryViewModel vm)
        {
            Category category = new();
            category.Name = vm.Name;
            category.Description = vm.Description;

            category = await _repository.AddAsync(category);

            SaveCategoryViewModel categoryVm = new();
            categoryVm.Id = category.Id;
            categoryVm.Name = category.Name;
            categoryVm.Description = category.Description;

            return categoryVm; 
        }

        public async Task Update(SaveCategoryViewModel vm)
        {
            Category category = new();
            category.Id = vm.Id;
            category.Name = vm.Name;
            category.Description = vm.Description;

            await _repository.UpdateAsync(category);
        }

        public async Task Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(category);
        }

        public async Task<SaveCategoryViewModel> GetByIdSaveViewModel(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            SaveCategoryViewModel vm = new();
            vm.Id = category.Id;
            vm.Name = category.Name;
            vm.Description = category.Description;
            
            return vm;
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var list = await _repository.GetAllWithInclude(new List<string> {"Products"});
            return list.Select(s=> new CategoryViewModel { 
                Name = s.Name,
                Description = s.Description,
                Id = s.Id,
                ProductQuantity = s.Products.Where(product => product.UserId == _userViewModel.Id).Count()
            }).ToList();
        }
    }
}
