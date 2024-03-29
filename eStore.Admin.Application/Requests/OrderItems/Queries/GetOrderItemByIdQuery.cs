﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using MediatR;

namespace eStore.Admin.Application.Requests.OrderItems.Queries;

public class GetOrderItemByIdQuery : IRequest<OrderItemResponse>
{
    public GetOrderItemByIdQuery(int orderItemId)
    {
        OrderItemId = orderItemId;
    }

    public int OrderItemId { get; }
}

public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, OrderItemResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderItemByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderItemResponse> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemId, false,
            cancellationToken);
        return _mapper.Map<OrderItemResponse>(orderItem);
    }
}