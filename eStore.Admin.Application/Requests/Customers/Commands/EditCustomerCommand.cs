using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Customers.Commands;

public class EditCustomerCommand : IRequest<CustomerResponse>
{
    public EditCustomerCommand(int customerId)
    {
        CustomerId = customerId;
    }

    public int CustomerId { get; }
    public CustomerDto Customer { get; set; }
}

public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommand, CustomerResponse>
{
    private readonly ILogger<EditCustomerCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EditCustomerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<EditCustomerCommandHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CustomerResponse> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, true,
            cancellationToken);
        if (customer is null)
        {
            throw new KeyNotFoundException($"The customer with id {request.CustomerId} has not been found.");
        }

        _mapper.Map(request.Customer, customer);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The customer with id {CustomerId} has been updated", customer.Id);

        return _mapper.Map<CustomerResponse>(customer);
    }
}