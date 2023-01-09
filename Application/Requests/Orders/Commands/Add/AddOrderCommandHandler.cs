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

namespace eStore_Admin.Application.Requests.Orders.Commands.Add
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<OrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request.Order);
            order.TimeStamp = _dateTimeService.Now();

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

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The new order with id {0} has been added.", order.Id);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}