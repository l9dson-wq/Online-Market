
using StoackApp.Core.Application.Interfaces.Repositories;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Categories;
using StoackApp.Core.Application.ViewModels.Products;
using StockApp.Core.Domain.Entities;


namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository CategoryRepository)
        {
            _repository = CategoryRepository;
        }

        public async Task Add(SaveCategoryViewModel vm)
        {
            Category category = new();
            category.Name = vm.Name;
            category.Description = vm.Description;        

            await _repository.AddAsync(category);
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
                ProductQuantity = s.Products.Count()
            }).ToList();
        }
    }
}
