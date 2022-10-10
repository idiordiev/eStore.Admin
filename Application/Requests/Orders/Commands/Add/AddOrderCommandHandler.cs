using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Add
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggingService _logger;
        private readonly IDateTimeService _dateTimeService;

        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger, IDateTimeService dateTimeService)
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
            
            cancellationToken.ThrowIfCancellationRequested();
            
            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            _logger.LogInformation("The new order with id {0} has been added.", order.Id);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}