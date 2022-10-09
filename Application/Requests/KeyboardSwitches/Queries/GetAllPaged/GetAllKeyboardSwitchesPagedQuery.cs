using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Queries.GetAllPaged
{
    public class GetAllKeyboardSwitchesPagedQuery : IRequest<IEnumerable<KeyboardSwitchResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }
}