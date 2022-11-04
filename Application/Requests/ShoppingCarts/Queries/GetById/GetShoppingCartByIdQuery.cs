using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.ShoppingCarts.Queries.GetById
{
    public class GetShoppingCartByIdQuery : IRequest<ShoppingCartResponse>
    {
        public GetShoppingCartByIdQuery(int shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }

        public int ShoppingCartId { get; }
    }
}