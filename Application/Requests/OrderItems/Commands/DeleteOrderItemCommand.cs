using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Commands;

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
    private readonly ILoggingService _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderItemCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        OrderItem orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemId, true,
            cancellationToken);
        if (orderItem is null)
        {
            return false;
        }

        Order order = await _unitOfWork.OrderRepository.GetByIdAsync(orderItem.OrderId, true, cancellationToken);
        if (order is null)
        {
            throw new KeyNotFoundException($"The order with the id {orderItem.OrderId} has not been found.");
        }

        order.OrderItems.Remove(orderItem);
        order.Total = order.OrderItems.Where(oi => !oi.IsDeleted).Sum(oi => oi.UnitPrice * oi.Quantity);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The order with id {0} has been updated, item deleted. New total is {1}.", order.Id,
            order.Total);

        return true;
    }
}