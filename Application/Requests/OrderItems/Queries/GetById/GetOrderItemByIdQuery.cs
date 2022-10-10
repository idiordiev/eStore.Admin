using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Queries.GetById
{
    public class GetOrderItemByIdQuery : IRequest<OrderItemResponse>
    {
        public GetOrderItemByIdQuery(int orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public int OrderItemId { get; }
    }
}