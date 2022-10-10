using System.Collections.Generic;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries.GetByFilterPaged
{
    public class GetOrdersByFilterPagedQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
        public OrderFilterModel FilterModel { get; set; }
    }
}