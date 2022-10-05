using System;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Delete
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, true, cancellationToken);
            if (customer is null)
                return false;

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation of deleting customer has been cancelled.");

            _unitOfWork.CustomerRepository.Delete(customer);
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}