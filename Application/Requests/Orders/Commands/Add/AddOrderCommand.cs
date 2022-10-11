using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Add
{
    public class AddOrderCommand : IRequest<OrderResponse>
    {
        public OrderDto Order { get; set; }
    }
}