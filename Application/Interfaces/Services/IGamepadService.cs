using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.DataTransferObjects;
using eStore_Admin.Application.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IGamepadService
    {
        Task<IEnumerable<GamepadDto>> GetAllGamepadsAsync();
        Task<IEnumerable<GamepadDto>> GetGamepadsByFilterAsync(GamepadFilterModel filter);
        Task<GamepadDto> GetGamepadByIdAsync(int gamepadId);
        Task AddGamepadAsync(GamepadDto gamepad);
        Task UpdateGamepadAsync(int gamepadId, GamepadDto gamepad);
        Task DeleteGamepadAsync(int gamepadId);
    }
}