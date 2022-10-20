using StoackApp.Core.Application.ViewModels.Products;
using StoackApp.Core.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoackApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel> 
        where SaveViewModel : class
        where ViewModel : class 

    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Update(SaveViewModel vm);
        Task Delete(int id);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<List<ViewModel>> GetAllViewModel();

    }
}
