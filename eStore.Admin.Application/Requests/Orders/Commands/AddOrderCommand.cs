using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Orders.Commands;

public class AddOrderCommand : IRequest<OrderResponse>
{
    public OrderDto Order { get; set; }
}

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderResponse>
{
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddOrderCommandHandler> _logger;

    public AddOrderCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IClock clock,
        ILogger<AddOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<OrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.Order);
        order.TimeStamp = _clock.UtcNow();

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

        _unitOfWork.OrderRepository.Add(order);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The new order with id {OrderId} has been added", order.Id);

        return _mapper.Map<OrderResponse>(order);
    }
}