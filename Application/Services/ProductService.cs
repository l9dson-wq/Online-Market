
using Microsoft.AspNetCore.Http;
using StoackApp.Core.Application.Helpers;
using StoackApp.Core.Application.Interfaces.Repositories;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Products;
using StoackApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;


namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public ProductService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = productRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<SaveProductViewModel> Add(SaveProductViewModel vm)
        {
            Product product = new();
            product.Name = vm.Name;
            product.Price = vm.Price;
            product.ImagePath = vm.ImagePath;
            product.Description = vm.Description;
            product.CategoryId = vm.CategoryId;
            product.UserId = _userViewModel.Id;

            product = await _repository.AddAsync(product);

            SaveProductViewModel productVm = new();
            productVm.Id = product.Id;
            productVm.Name = product.Name;
            productVm.Price = product.Price;
            productVm.Description = product.Description;
            productVm.CategoryId = product.CategoryId;

            return productVm; 
        }

        public async Task Update(SaveProductViewModel vm)
        {
            Product product = new();
            product.Id = vm.Id;
            product.Name = vm.Name;
            product.Price = vm.Price;
            product.ImagePath = vm.ImagePath;
            product.Description = vm.Description;
            product.CategoryId = vm.CategoryId;

            await _repository.UpdateAsync(product);
        }

        public async Task Delete(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(product);
        }

        public async Task<SaveProductViewModel> GetByIdSaveViewModel(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            SaveProductViewModel vm = new();
            vm.Id = product.Id;
            vm.Name = product.Name;
            vm.Description = product.Description;
            vm.CategoryId = product.CategoryId;
            vm.Price = product.Price;
            vm.ImagePath = product.ImagePath;
            
            return vm;
        }

        public async Task<List<ProductViewModel>> GetAllViewModel()
        {
            var list = await _repository.GetAllWithInclude(new List<string> {"Category"});
            return list.Where(product => product.UserId == _userViewModel.Id).Select(s=> new ProductViewModel { 
                Name = s.Name,
                Description = s.Description,
                Id = s.Id,
                Price = s.Price,
                ImagePath = s.ImagePath,
                CategoryName = s.Category.Name,
            }).ToList();
        }

        public async Task<List<ProductViewModel>> GetAllViewModelWithFilters(FilterProductViewModel filters)
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "Category" });

            var listViewModels = list.Where(product => product.UserId == _userViewModel.Id).Select(s => new ProductViewModel
            {
                Name = s.Name,
                Description = s.Description,
                Id = s.Id,
                Price = s.Price,
                ImagePath = s.ImagePath,
                CategoryName = s.Category.Name,
                CategoryId = s.Category.Id
            }).ToList();

            if(filters.CategoryId != null)
            {
                listViewModels = listViewModels.Where(product => product.CategoryId == filters.CategoryId.Value).ToList();
            }

            return listViewModels;
        }
    }
}
