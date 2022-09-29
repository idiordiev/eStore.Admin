using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.DataTransferObjects;
using eStore_Admin.Application.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IMouseService
    {
        Task<IEnumerable<MouseDto>> GetAllMousesAsync();
        Task<IEnumerable<MouseDto>> GetMousesByFilterAsync(MouseFilterModel filter);
        Task<MouseDto> GetMouseByIdAsync(int mouseId);
        Task AddMouseAsync(MouseDto mouse);
        Task UpdateMouseAsync(int mouseId, MouseDto mouse);
        Task DeleteMouseAsync(int mouseId);
    }
}