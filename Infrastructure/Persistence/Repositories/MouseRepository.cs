using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Infrastructure.Persistence.Repositories
{
    public class MouseRepository : RepositoryBase<Mouse>, IMouseRepository
    {
        public MouseRepository(ApplicationContext context) : base(context)
        {
        }
    }
}