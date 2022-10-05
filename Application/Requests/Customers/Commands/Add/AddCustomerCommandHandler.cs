using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Add
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerResponse> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation of adding new customer has been cancelled.");
            
            _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            return _mapper.Map<CustomerResponse>(customer);
        }
    }
}