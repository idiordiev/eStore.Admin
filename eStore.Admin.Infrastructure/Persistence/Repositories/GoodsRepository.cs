using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class GoodsRepository : Repository<Goods>, IGoodsRepository
{
    public GoodsRepository(ApplicationContext context) : base(context)
    {
    }
}