using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries;

public class GetOrdersByFilterPagedQuery : IRequest<IEnumerable<OrderResponse>>
{
    public PagingParameters PagingParameters { get; set; }
    public OrderFilterModel FilterModel { get; set; }
}

public class GetOrdersByFilterPagedQueryHandler : IRequestHandler<GetOrdersByFilterPagedQuery,
    IEnumerable<OrderResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPredicateFactory<Order, OrderFilterModel> _predicateFactory;
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
        IPredicateFactory<Order, OrderFilterModel> predicateFactory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _predicateFactory = predicateFactory;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByFilterPagedQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = _predicateFactory.CreateExpression(request.FilterModel);
        var orders = await _unitOfWork.OrderRepository.GetByConditionPagedAsync(predicate, request.PagingParameters,
            false, cancellationToken);
        return _mapper.Map<IEnumerable<OrderResponse>>(orders);
    }
}