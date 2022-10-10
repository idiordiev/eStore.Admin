using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Queries.GetByOrderId
{
    public class GetOrderItemsByOrderIdPagedQuery : IRequest<IEnumerable<OrderItemResponse>>
    {
        public GetOrderItemsByOrderIdPagedQuery(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
        public PagingParameters PagingParameters { get; set; }
    }
}