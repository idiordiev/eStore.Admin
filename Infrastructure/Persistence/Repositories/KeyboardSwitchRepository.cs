using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Infrastructure.Persistence.Repositories;

public class KeyboardSwitchRepository : RepositoryBase<KeyboardSwitch>, IKeyboardSwitchRepository
{
    public KeyboardSwitchRepository(ApplicationContext context) : base(context)
    {
    }
}