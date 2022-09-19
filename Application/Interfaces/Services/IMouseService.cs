using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.DataTransferObjects;
using eStore_Admin.Application.Interfaces.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IMouseService
    {
        Task<IEnumerable<IMouseDto>> GetAllMousesAsync();
        Task<IEnumerable<IMouseDto>> GetMousesByFilterAsync(IMouseFilterModel filter);
        Task<IMouseDto> GetMouseByIdAsync(int mouseId);
        Task AddMouseAsync(IMouseDto mouse);
        Task UpdateMouseAsync(int mouseId, IMouseDto mouse);
        Task DeleteMouseAsync(int mouseId);
    }
}