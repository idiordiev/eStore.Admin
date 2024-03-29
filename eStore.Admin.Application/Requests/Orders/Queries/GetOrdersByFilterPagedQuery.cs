﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Orders.Queries;

public class GetOrdersByFilterPagedQuery : IRequest<IEnumerable<OrderResponse>>
{
    public PagingParameters PagingParameters { get; set; }
    public OrderFilterModel FilterModel { get; set; }
}

public class GetOrdersByFilterPagedQueryHandler : IRequestHandler<GetOrdersByFilterPagedQuery,
    IEnumerable<OrderResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByFilterPagedQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = request.FilterModel.CreateExpression();
        var orders = await _unitOfWork.OrderRepository.GetByConditionPagedAsync(predicate, request.PagingParameters,
            false, cancellationToken);
        
        return _mapper.Map<IEnumerable<OrderResponse>>(orders);
    }
}