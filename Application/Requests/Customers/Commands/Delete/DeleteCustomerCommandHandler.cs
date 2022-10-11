using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Delete
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ILoggingService _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, true, cancellationToken);
            if (customer is null)
            {
                _logger.LogInformation("The customer with id {0} has not been found.", request.CustomerId);
                return false;
            }

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.CustomerRepository.Delete(customer);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The customer with id {0} has been deleted.", customer.Id);

            return true;
        }
    }
}