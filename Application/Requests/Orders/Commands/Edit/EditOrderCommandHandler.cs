using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Edit
{
    public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, OrderResponse>
    {
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderResponse> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, true, cancellationToken);
            if (order is null)
            {
                throw new KeyNotFoundException($"The order with the id {request.OrderId} has not been found.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            _mapper.Map(request.Order, order);

            foreach (OrderItemDto item in request.Order.ItemsToAdd)
            {
                Goods goods = await _unitOfWork.GoodsRepository.GetByIdAsync(item.GoodsId, false, cancellationToken);
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

            _logger.LogInformation("The order with id {0} has been edited.", order.Id);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}