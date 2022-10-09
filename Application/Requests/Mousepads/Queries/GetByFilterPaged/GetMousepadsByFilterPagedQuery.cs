using System.Collections.Generic;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Queries.GetByFilterPaged
{
    public class GetMousepadsByFilterPagedQuery : IRequest<IEnumerable<MousepadResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
        public MousepadFilterModel FilterModel { get; set; }
    }
}