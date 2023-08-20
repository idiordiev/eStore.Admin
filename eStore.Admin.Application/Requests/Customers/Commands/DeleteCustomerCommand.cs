using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Customers.Commands;

public class DeleteCustomerCommand : IRequest<bool>
{
    public DeleteCustomerCommand(int customerId)
    {
        CustomerId = customerId;
    }

    public int CustomerId { get; }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCustomerCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, true,
            cancellationToken);
        if (customer is null)
        {
            _logger.LogInformation("The customer with id {CustomerId} has not been found", request.CustomerId);
            return false;
        }

        _unitOfWork.CustomerRepository.Delete(customer);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The customer with id {CustomerId} has been deleted", customer.Id);

        return true;
    }
}