using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class MousepadRepository : RepositoryBase<Mousepad>, IMousepadRepository
{
    public MousepadRepository(ApplicationContext context) : base(context)
    {
    }
}