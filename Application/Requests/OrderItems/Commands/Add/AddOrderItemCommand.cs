using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Commands.Add
{
    public class AddOrderItemCommand : IRequest<OrderResponse>
    {
        public OrderItemRequest OrderItem { get; set; }
    }
}