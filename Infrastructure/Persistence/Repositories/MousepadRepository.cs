using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Infrastructure.Persistence.Repositories;

public class MousepadRepository : RepositoryBase<Mousepad>, IMousepadRepository
{
    public MousepadRepository(ApplicationContext context) : base(context)
    {
    }
}