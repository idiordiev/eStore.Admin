using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class KeyboardRepository : Repository<Keyboard>, IKeyboardRepository
{
    public KeyboardRepository(ApplicationContext context) : base(context)
    {
    }
}