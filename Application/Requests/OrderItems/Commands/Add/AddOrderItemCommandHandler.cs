using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Commands.Add
{
    public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, OrderResponse>
    {
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderResponse> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            Order order =
                await _unitOfWork.OrderRepository.GetByIdWithOrderItemsAsync(request.OrderId, true, cancellationToken);
            if (order is null)
            {
                throw new KeyNotFoundException($"The order with the id {request.OrderId} has not been found.");
            }

            Goods goods =
                await _unitOfWork.GoodsRepository.GetByIdAsync(request.OrderItem.GoodsId, false, cancellationToken);
            if (goods is null)
            {
                throw new KeyNotFoundException(
                    $"The goods with the id {request.OrderItem.GoodsId} has not been found.");
            }

            var orderItem = new OrderItem
            {
                Goods = goods,
                Order = order,
                IsDeleted = false,
                Quantity = request.OrderItem.Quantity,
                UnitPrice = goods.Price
            };

            cancellationToken.ThrowIfCancellationRequested();

            order.OrderItems.Add(orderItem);
            order.Total = order.OrderItems.Where(oi => !oi.IsDeleted).Sum(oi => oi.UnitPrice * oi.Quantity);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The order with id {0} has been updated, new item added. New total is {1}.",
                order.Id, order.Total);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}