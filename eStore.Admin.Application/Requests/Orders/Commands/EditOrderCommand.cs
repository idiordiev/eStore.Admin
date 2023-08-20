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

namespace eStore.Admin.Application.Requests.Orders.Commands;

public class EditOrderCommand : IRequest<OrderResponse>
{
    public EditOrderCommand(int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
    public OrderDto Order { get; set; }
}

public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, OrderResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditOrderCommandHandler> _logger;

    public EditOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EditOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OrderResponse> Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, true, cancellationToken);
        if (order is null)
        {
            throw new KeyNotFoundException($"The order with the id {request.OrderId} has not been found.");
        }

        _mapper.Map(request.Order, order);

        foreach (var item in request.Order.ItemsToAdd)
        {
            var goods = await _unitOfWork.GoodsRepository.GetByIdAsync(item.GoodsId, false, cancellationToken);
            if (goods is null)
            {
                throw new KeyNotFoundException($"The goods with id {item.GoodsId} has not been found.");
            }

            order.OrderItems.Add(new OrderItem
            {
                IsDeleted = item.IsDeleted,
                Goods = goods,
                Quantity = item.Quantity,
                UnitPrice = goods.Price
            });
        }

        order.Total = order.OrderItems.Where(oi => !oi.IsDeleted).Sum(oi => oi.UnitPrice * oi.Quantity);

        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The order with id {OrderId} has been edited", order.Id);

        return _mapper.Map<OrderResponse>(order);
    }
}