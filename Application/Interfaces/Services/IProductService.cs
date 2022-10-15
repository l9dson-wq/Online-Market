using StoackApp.Core.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoackApp.Core.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task Add(SaveProductViewModel vm);
        Task Update(SaveProductViewModel vm);
        Task Delete(int id);
        Task<SaveProductViewModel> GetByIdSaveViewModel(int id);
        Task<List<ProductViewModel>> GetAllViewModel();
    }
}
