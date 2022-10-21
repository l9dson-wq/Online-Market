using StoackApp.Core.Application.ViewModels.Products;
using StoackApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoackApp.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm); 
    }
}
