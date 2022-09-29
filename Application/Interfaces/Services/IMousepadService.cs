using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.DataTransferObjects;
using eStore_Admin.Application.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IMousepadService
    {
        Task<IEnumerable<MousepadDto>> GetAllMousepadsAsync();
        Task<IEnumerable<MousepadDto>> GetMousepadsByFilterAsync(MousepadFilterModel filter);
        Task<MousepadDto> GetMousepadByIdAsync(int mousepadId);
        Task AddMousepadAsync(MousepadDto mousepad);
        Task UpdateMousepadAsync(int mousepadId, MousepadDto mousepad);
        Task DeleteMousepadAsync(int mousepadId);
    }
}