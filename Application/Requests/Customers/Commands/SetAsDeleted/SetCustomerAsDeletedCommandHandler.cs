using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.SetAsDeleted
{
    public class SetCustomerAsDeletedCommandHandler : IRequestHandler<SetCustomerAsDeletedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetCustomerAsDeletedCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SetCustomerAsDeletedCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, true, cancellationToken);
            if (customer is null)
                return false;

            customer.IsDeleted = true;
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}