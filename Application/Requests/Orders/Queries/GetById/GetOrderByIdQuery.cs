using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries.GetById
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}