using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.DataTransferObjects;
using eStore_Admin.Application.Interfaces.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IMousepadService
    {
        Task<IEnumerable<IMousepadDto>> GetAllMousepadsAsync();
        Task<IEnumerable<IMousepadDto>> GetMousepadsByFilterAsync(IMousepadFilterModel filter);
        Task<IMousepadDto> GetMousepadByIdAsync(int mousepadId);
        Task AddMousepadAsync(IMousepadDto mousepad);
        Task UpdateMousepadAsync(int mousepadId, IMousepadDto mousepad);
        Task DeleteMousepadAsync(int mousepadId);
    }
}