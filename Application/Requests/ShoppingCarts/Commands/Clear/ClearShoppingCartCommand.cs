using MediatR;

namespace eStore_Admin.Application.Requests.ShoppingCarts.Commands.Clear
{
    public class ClearShoppingCartCommand : IRequest<bool>
    {
        public ClearShoppingCartCommand(int shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }

        public int ShoppingCartId { get; }
    }
}