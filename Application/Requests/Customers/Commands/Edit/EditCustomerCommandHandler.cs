using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Edit
{
    public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommand, CustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggingService _logger;

        public EditCustomerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggingService logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CustomerResponse> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, true, cancellationToken);
            if (customer is null)
                throw new KeyNotFoundException($"The customer with id {request.CustomerId} has not been found.");

            cancellationToken.ThrowIfCancellationRequested();

            _mapper.Map(request.Customer, customer);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            _logger.LogInformation("The customer with id {0} has been updated.", customer.Id);

            return _mapper.Map<CustomerResponse>(customer);
        }
    }
}