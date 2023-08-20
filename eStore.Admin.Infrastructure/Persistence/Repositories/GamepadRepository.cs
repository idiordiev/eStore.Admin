using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class GamepadRepository : Repository<Gamepad>, IGamepadRepository
{
    public GamepadRepository(ApplicationContext context) : base(context)
    {
    }
}