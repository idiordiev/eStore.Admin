using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class KeyboardSwitchRepository : Repository<KeyboardSwitch>, IKeyboardSwitchRepository
{
    public KeyboardSwitchRepository(ApplicationContext context) : base(context)
    {
    }
}