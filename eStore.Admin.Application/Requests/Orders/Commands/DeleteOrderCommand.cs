using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Orders.Commands;

public class DeleteOrderCommand : IRequest<bool>
{
    public DeleteOrderCommand(int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, true, cancellationToken);
        if (order is null)
        {
            _logger.LogInformation("The order with id {OrderId} has not been found", request.OrderId);
            return false;
        }

        _unitOfWork.OrderRepository.Delete(order);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The order with id {OrderId} has been deleted", order.Id);

        return true;
    }
}