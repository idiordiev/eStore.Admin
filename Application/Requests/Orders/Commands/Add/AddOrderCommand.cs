using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Add
{
    public class AddOrderCommand : IRequest<OrderResponse>
    {
        public OrderRequest Order { get; set; }
    }
}