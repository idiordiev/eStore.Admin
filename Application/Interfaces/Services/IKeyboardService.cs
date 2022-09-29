using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.DataTransferObjects;
using eStore_Admin.Application.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IKeyboardService
    {
        Task<IEnumerable<KeyboardDto>> GetAllKeyboardsAsync();
        Task<IEnumerable<KeyboardDto>> GetKeyboardsByFilterAsync(KeyboardFilterModel filter);
        Task<KeyboardDto> GetKeyboardByIdAsync(int keyboardId);
        Task AddKeyboardAsync(KeyboardDto keyboard);
        Task UpdateKeyboardAsync(int keyboardId, KeyboardDto keyboard);
        Task DeleteKeyboardAsync(int keyboardId);
    }
}