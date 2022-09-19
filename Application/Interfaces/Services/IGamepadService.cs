using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.DataTransferObjects;
using eStore_Admin.Application.Interfaces.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IGamepadService
    {
        Task<IEnumerable<IGamepadDto>> GetAllGamepadsAsync();
        Task<IEnumerable<IGamepadDto>> GetGamepadsByFilterAsync(IGamepadFilterModel filter);
        Task<IGamepadDto> GetGamepadByIdAsync(int gamepadId);
        Task AddGamepadAsync(IGamepadDto gamepad);
        Task UpdateGamepadAsync(int gamepadId, IGamepadDto gamepad);
        Task DeleteGamepadAsync(int gamepadId);
    }
}