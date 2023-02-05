using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Infrastructure.Persistence.Repositories;

public class GoodsRepository : RepositoryBase<Goods>, IGoodsRepository
{
    public GoodsRepository(ApplicationContext context) : base(context)
    {
    }
}