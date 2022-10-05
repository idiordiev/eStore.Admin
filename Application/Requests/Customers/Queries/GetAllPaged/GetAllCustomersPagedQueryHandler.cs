using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetAllPaged
{
    public class GetAllCustomersPagedQueryHandler : IRequestHandler<GetAllCustomersPagedQuery, IEnumerable<CustomerResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomersPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponse>> Handle(GetAllCustomersPagedQuery request, CancellationToken cancellationToken)
        {
            var pagingParameters = new PagingParameters(request.PageSize, request.PageNumber);
            var customers = await _unitOfWork.CustomerRepository.GetAllPagedAsync(pagingParameters, false, cancellationToken);
            var response = _mapper.Map<IEnumerable<CustomerResponse>>(customers);
            return response;
        }
    }
}