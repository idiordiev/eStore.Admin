using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Delete
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}