using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.DataTransferObjects;
using eStore_Admin.Application.Interfaces.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IKeyboardService
    {
        Task<IEnumerable<IKeyboardDto>> GetAllKeyboardsAsync();
        Task<IEnumerable<IKeyboardDto>> GetKeyboardsByFilterAsync(IKeyboardFilterModel filter);
        Task<IKeyboardDto> GetKeyboardByIdAsync(int keyboardId);
        Task AddKeyboardAsync(IKeyboardDto keyboard);
        Task UpdateKeyboardAsync(int keyboardId, IKeyboardDto keyboard);
        Task DeleteKeyboardAsync(int keyboardId);
    }
}