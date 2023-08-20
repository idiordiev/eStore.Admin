using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class MouseRepository : Repository<Mouse>, IMouseRepository
{
    public MouseRepository(ApplicationContext context) : base(context)
    {
    }
}