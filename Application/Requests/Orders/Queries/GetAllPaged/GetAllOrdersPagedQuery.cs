using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries.GetAllPaged
{
    public class GetAllOrdersPagedQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }
}