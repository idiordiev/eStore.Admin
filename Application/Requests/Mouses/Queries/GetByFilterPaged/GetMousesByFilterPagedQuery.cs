using System.Collections.Generic;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries.GetByFilterPaged
{
    public class GetMousesByFilterPagedQuery : IRequest<IEnumerable<MouseResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
        public MouseFilterModel FilterModel { get; set; }
    }
}