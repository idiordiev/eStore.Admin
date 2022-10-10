using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Commands.Edit
{
    public class EditOrderItemCommand : IRequest<OrderResponse>
    {
        public EditOrderItemCommand(int orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public int OrderItemId { get; }
        public OrderItemRequest OrderItem { get; set; }
    }
}