using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetByFilterPaged
{
    public class GetCustomerByFilterPagedQueryHandler : IRequestHandler<GetCustomerByFilterPagedQuery, IEnumerable<CustomerResponse>>
    {
        private readonly ICustomerFilterExpressionFactory _filterExpressionFactory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ICustomerFilterExpressionFactory filterExpressionFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterExpressionFactory = filterExpressionFactory;
        }

        public async Task<IEnumerable<CustomerResponse>> Handle(GetCustomerByFilterPagedQuery request,
            CancellationToken cancellationToken)
        {
            var predicate = _filterExpressionFactory.CreateExpression(request.FilterModel);
            var customers = await _unitOfWork.CustomerRepository.GetByConditionPagedAsync(predicate, request.PagingParameters, false,
                cancellationToken);

            return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
        }
    }
}