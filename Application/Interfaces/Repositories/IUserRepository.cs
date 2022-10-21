using StoackApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoackApp.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);
    }
}
