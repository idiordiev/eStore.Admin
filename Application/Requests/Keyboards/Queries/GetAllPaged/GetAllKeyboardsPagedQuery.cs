using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetAllPaged
{
    public class GetAllKeyboardsPagedQuery : IRequest<IEnumerable<KeyboardResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}