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

namespace eStore_Admin.Application.Requests.OrderItems.Commands.Edit
{
    public class EditOrderItemCommandHandler : IRequestHandler<EditOrderItemCommand, OrderResponse>
    {
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderResponse> Handle(EditOrderItemCommand request, CancellationToken cancellationToken)
        {
            OrderItem orderItem =
                await _unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemId, true, cancellationToken);
            if (orderItem is null)
            {
                throw new KeyNotFoundException($"The order item with the id {request.OrderItemId} has not been found.");
            }

            Order order = await _unitOfWork.OrderRepository.GetByIdAsync(orderItem.OrderId, true, cancellationToken);
            if (order is null)
            {
                throw new KeyNotFoundException($"The order with the id {orderItem.OrderId} has not been found.");
            }

            if (orderItem.GoodsId != request.OrderItem.GoodsId)
            {
                Goods goods =
                    await _unitOfWork.GoodsRepository.GetByIdAsync(request.OrderItem.GoodsId, false, cancellationToken);
                if (goods is null)
                {
                    throw new KeyNotFoundException(
                        $"The goods with the id {request.OrderItem.GoodsId} has not been found.");
                }

                orderItem.GoodsId = request.OrderItem.GoodsId;
                orderItem.Goods = goods;
                orderItem.UnitPrice = goods.Price;
            }

            orderItem.Quantity = request.OrderItem.Quantity;

            cancellationToken.ThrowIfCancellationRequested();

            order.Total = order.OrderItems.Where(oi => !oi.IsDeleted).Sum(oi => oi.UnitPrice * oi.Quantity);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The order with id {0} has been updated, item deleted. New total is {1}.", order.Id,
                order.Total);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}