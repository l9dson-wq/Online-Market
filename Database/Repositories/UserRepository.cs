using Microsoft.EntityFrameworkCore;
using StoackApp.Core.Application.Helpers;
using StoackApp.Core.Application.Interfaces.Repositories;
using StoackApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;
using StockApp.Infrastructure.Persistence.Context;

namespace Application.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.password = PasswordEncryption.ComputeSha256Hash(entity.password);
            return await base.AddAsync(entity);
        }

        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypy = PasswordEncryption.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.password == passwordEncrypy);

            return user;
        } 
    }
}
