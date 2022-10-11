using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Add
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerResponse>
    {
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerResponse> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The new customer with id {0} has been added.", customer.Id);

            return _mapper.Map<CustomerResponse>(customer);
        }
    }
}