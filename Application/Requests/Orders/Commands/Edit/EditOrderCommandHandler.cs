using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Edit
{
    public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggingService _logger;

        public EditOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderResponse> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, true, cancellationToken);
            if (order is null)
                throw new KeyNotFoundException($"The order with the id {request.OrderId} has not been found.");

            cancellationToken.ThrowIfCancellationRequested();

            _mapper.Map(request.Order, order);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            _logger.LogInformation("The order with id {0} has been edited.", order.Id);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}