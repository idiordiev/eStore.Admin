using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries
{
    public class GetAllCustomersPagedQuery : IRequest<IEnumerable<CustomerResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }

    public class GetAllCustomersPagedQueryHandler : IRequestHandler<GetAllCustomersPagedQuery,
            IEnumerable<CustomerResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCustomersPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponse>> Handle(GetAllCustomersPagedQuery request,
            CancellationToken cancellationToken)
        {
            var customers =
                await _unitOfWork.CustomerRepository.GetAllPagedAsync(request.PagingParameters, false,
                    cancellationToken);
            return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
        }
    }
}