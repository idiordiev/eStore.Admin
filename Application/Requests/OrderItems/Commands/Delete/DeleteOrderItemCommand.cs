using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Commands.Delete
{
    public class DeleteOrderItemCommand : IRequest<bool>
    {
        public DeleteOrderItemCommand(int orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public int OrderItemId { get; }
    }
}