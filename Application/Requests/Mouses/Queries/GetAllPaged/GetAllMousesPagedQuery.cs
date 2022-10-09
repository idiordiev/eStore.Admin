using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries.GetAllPaged
{
    public class GetAllMousesPagedQuery : IRequest<IEnumerable<MouseResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }
}