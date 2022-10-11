using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Commands.Add
{
    public class AddOrderItemCommand : IRequest<OrderResponse>
    {
        public OrderItemDto OrderItem { get; set; }
    }
}