using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Infrastructure.Persistence.Repositories;

public class GamepadRepository : RepositoryBase<Gamepad>, IGamepadRepository
{
    public GamepadRepository(ApplicationContext context) : base(context)
    {
    }
}