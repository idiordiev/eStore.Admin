using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Edit
{
    public class EditOrderCommand : IRequest<OrderResponse>
    {
        public EditOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
        public OrderDto Order { get; set; }
    }
}