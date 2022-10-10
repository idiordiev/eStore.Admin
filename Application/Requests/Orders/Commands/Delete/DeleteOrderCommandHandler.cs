using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Commands.Delete
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggingService _logger;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, true, cancellationToken);
            if (order is null)
            {
                _logger.LogInformation("The order with id {0} has not been found.", request.OrderId);
                return false;
            }

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            _logger.LogInformation("The order with id {0} has been deleted.", order.Id);

            return true;
        }
    }
}