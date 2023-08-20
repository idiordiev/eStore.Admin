using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.OrderItems.Commands;

public class DeleteOrderItemCommand : IRequest<bool>
{
    public DeleteOrderItemCommand(int orderItemId)
    {
        OrderItemId = orderItemId;
    }

    public int OrderItemId { get; }
}

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOrderItemCommandHandler> _logger;

    public DeleteOrderItemCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteOrderItemCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemId, true,
            cancellationToken);
        if (orderItem is null)
        {
            return false;
        }

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderItem.OrderId, true, cancellationToken);
        if (order is null)
        {
            throw new KeyNotFoundException($"The order with the id {orderItem.OrderId} has not been found.");
        }

        order.OrderItems.Remove(orderItem);
        order.Total = order.OrderItems.Where(oi => !oi.IsDeleted).Sum(oi => oi.UnitPrice * oi.Quantity);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The order with id {OrderId} has been updated, item with id {OrderItemId} deleted",
            order.Id, orderItem.Id);

        return true;
    }
}