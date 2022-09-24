using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Infrastructure.Persistence.Repositories
{
    public class KeyboardRepository : RepositoryBase<Keyboard>, IKeyboardRepository
    {
        public KeyboardRepository(ApplicationContext context) : base(context)
        {
        }
    }
}