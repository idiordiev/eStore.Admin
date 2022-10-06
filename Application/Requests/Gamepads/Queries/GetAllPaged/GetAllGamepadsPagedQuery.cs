using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetAllPaged
{
    public class GetAllGamepadsPagedQuery : IRequest<IEnumerable<GamepadResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}