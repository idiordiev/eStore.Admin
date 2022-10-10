using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries.GetByCustomerIdPaged
{
    public class GetOrdersByCustomerIdPagedQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public GetOrdersByCustomerIdPagedQuery(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
        public PagingParameters PagingParameters { get; set; }
    }
}