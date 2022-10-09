using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Queries.GetAllPaged
{
    public class GetAllMousepadPagedQuery : IRequest<IEnumerable<MousepadResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }
}