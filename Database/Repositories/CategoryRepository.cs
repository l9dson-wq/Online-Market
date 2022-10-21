using Microsoft.EntityFrameworkCore;
using StoackApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Domain.Entities;
using StockApp.Infrastructure.Persistence.Context;

namespace Application.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationContext _dbContext;

        public CategoryRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
