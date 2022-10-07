using System.Collections.Generic;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetByFilterPaged
{
    public class GetGamepadsByFilterPagedQuery : IRequest<IEnumerable<GamepadResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
        public GamepadFilterModel FilterModel { get; set; }
    }
}