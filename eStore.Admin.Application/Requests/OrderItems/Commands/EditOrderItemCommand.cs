using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.OrderItems.Commands;

public class EditOrderItemCommand : IRequest<OrderResponse>
{
    public EditOrderItemCommand(int orderItemId)
    {
        OrderItemId = orderItemId;
    }

    public int OrderItemId { get; }
    public OrderItemDto OrderItem { get; set; }
}

public class EditOrderItemCommandHandler : IRequestHandler<EditOrderItemCommand, OrderResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditOrderItemCommandHandler> _logger;

    public EditOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EditOrderItemCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OrderResponse> Handle(EditOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemId, true, cancellationToken);
        if (orderItem is null)
        {
            throw new KeyNotFoundException($"The order item with the id {request.OrderItemId} has not been found.");
        }

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderItem.OrderId, true, cancellationToken);
        if (order is null)
        {
            throw new KeyNotFoundException($"The order with the id {orderItem.OrderId} has not been found.");
        }

        if (orderItem.GoodsId != request.OrderItem.GoodsId)
        {
            var goods = await _unitOfWork.GoodsRepository.GetByIdAsync(request.OrderItem.GoodsId, false, cancellationToken);
            if (goods is null)
            {
                throw new KeyNotFoundException($"The goods with the id {request.OrderItem.GoodsId} has not been found.");
            }

            orderItem.GoodsId = request.OrderItem.GoodsId;
            orderItem.Goods = goods;
            orderItem.UnitPrice = goods.Price;
        }

        orderItem.Quantity = request.OrderItem.Quantity;
        order.Total = order.OrderItems.Where(oi => !oi.IsDeleted).Sum(oi => oi.UnitPrice * oi.Quantity);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The order with id {OrderId} has been updated, item with Id {OrderItemId} deleted",
            order.Id, orderItem.Id);

        return _mapper.Map<OrderResponse>(order);
    }
}