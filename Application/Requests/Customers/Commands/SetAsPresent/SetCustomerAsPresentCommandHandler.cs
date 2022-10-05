using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.SetAsPresent
{
    public class SetCustomerAsPresentCommandHandler : IRequestHandler<SetCustomerAsPresentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetCustomerAsPresentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SetCustomerAsPresentCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, true, cancellationToken);
            if (customer is null)
                return false;

            customer.IsDeleted = false;
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}