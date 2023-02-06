using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Interfaces.Services;
using eStore.Admin.Domain.Entities;
using MediatR;

namespace eStore.Admin.Application.Requests.ShoppingCarts.Commands;

public class ClearShoppingCartCommand : IRequest<bool>
{
    public ClearShoppingCartCommand(int shoppingCartId)
    {
        ShoppingCartId = shoppingCartId;
    }

    public int ShoppingCartId { get; }
}

public class ClearShoppingCartCommandHandler : IRequestHandler<ClearShoppingCartCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggingService _logger;

    public ClearShoppingCartCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(ClearShoppingCartCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart shoppingCart = await _unitOfWork.ShoppingCartRepository.GetByIdWithItemsAsync(
            request.ShoppingCartId, true, cancellationToken);
        if (shoppingCart is null)
        {
            _logger.LogInformation("The shopping cart with id {0} has not been found.", request.ShoppingCartId);
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();

        shoppingCart.Goods.Clear();
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The shopping cart with id {0} has been cleared.", shoppingCart.Id);

        return true;
    }
}