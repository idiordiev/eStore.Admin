using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.OrderItems.Commands;

public class AddOrderItemCommand : IRequest<OrderResponse>
{
    public AddOrderItemCommand(int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
    public OrderItemDto OrderItem { get; set; }
}

public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, OrderResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddOrderItemCommandHandler> _logger;

    public AddOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddOrderItemCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OrderResponse> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdWithOrderItemsAsync(request.OrderId, true,
            cancellationToken);
        if (order is null)
        {
            throw new KeyNotFoundException($"The order with the id {request.OrderId} has not been found.");
        }

        var goods = await _unitOfWork.GoodsRepository.GetByIdAsync(request.OrderItem.GoodsId, false,
            cancellationToken);
        if (goods is null)
        {
            throw new KeyNotFoundException($"The goods with the id {request.OrderItem.GoodsId} has not been found.");
        }

        var orderItem = new OrderItem
        {
            Goods = goods,
            Order = order,
            IsDeleted = false,
            Quantity = request.OrderItem.Quantity,
            UnitPrice = goods.Price
        };

        order.OrderItems.Add(orderItem);
        order.Total = order.OrderItems.Where(oi => !oi.IsDeleted)
            .Sum(oi => oi.UnitPrice * oi.Quantity);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The order with id {OrderId} has been updated, new item with Id {OrderItemId} added",
            order.Id, orderItem.Id);

        return _mapper.Map<OrderResponse>(order);
    }
}