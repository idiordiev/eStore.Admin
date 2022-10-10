using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Commands.Delete
{
    public class DeleteOrderItemCommand : IRequest<OrderResponse>
    {
        public DeleteOrderItemCommand(int orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public int OrderItemId { get; }
    }
}