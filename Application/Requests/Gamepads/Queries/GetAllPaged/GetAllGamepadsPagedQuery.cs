using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetAllPaged
{
    public class GetAllGamepadsPagedQuery : IRequest<IEnumerable<GamepadResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }
}