using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;
using MediatR;

namespace eStore.Admin.Application.Requests.Customers.Queries;

public class GetCustomerByFilterPagedQuery : IRequest<IEnumerable<CustomerResponse>>
{
    public PagingParameters PagingParameters { get; set; }
    public CustomerFilterModel FilterModel { get; set; }
}

public class GetCustomerByFilterPagedQueryHandler : IRequestHandler<GetCustomerByFilterPagedQuery,
    IEnumerable<CustomerResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPredicateFactory<Customer, CustomerFilterModel> _predicateFactory;
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
        IPredicateFactory<Customer, CustomerFilterModel> predicateFactory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _predicateFactory = predicateFactory;
    }

    public async Task<IEnumerable<CustomerResponse>> Handle(GetCustomerByFilterPagedQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = _predicateFactory.CreateExpression(request.FilterModel);
        var customers = await _unitOfWork.CustomerRepository.GetByConditionPagedAsync(predicate,
            request.PagingParameters, false, cancellationToken);

        return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
    }
}