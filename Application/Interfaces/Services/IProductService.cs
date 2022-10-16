using StoackApp.Core.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoackApp.Core.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<SaveProductViewModel, ProductViewModel>
    {
        Task<List<ProductViewModel>> GetAllViewModelWithFilters(FilterProductViewModel filters);
    }
}
