using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetAllPaged
{
    public class GetAllKeyboardsPagedQuery : IRequest<IEnumerable<KeyboardResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }
}