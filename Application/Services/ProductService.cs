
using StoackApp.Core.Application.Interfaces.Repositories;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Products;
using StockApp.Core.Domain.Entities;


namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public async Task Add(SaveProductViewModel vm)
        {
            Product product = new();
            product.Name = vm.Name;
            product.Price = vm.Price;
            product.ImagePath = vm.ImagePath;
            product.Description = vm.Description;
            product.CategoryId = vm.CategoryId;           

            await _repository.AddAsync(product);
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
            var list = await _repository.GetAllAsync();
            return list.Select(s=> new ProductViewModel { 
                Name = s.Name,
                Description = s.Description,
                Id = s.Id,
                Price = s.Price,
                ImagePath = s.ImagePath
            }).ToList();
        }
    }
}
