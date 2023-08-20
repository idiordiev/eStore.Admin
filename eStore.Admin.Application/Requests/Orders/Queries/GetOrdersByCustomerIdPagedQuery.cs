using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Orders.Queries;

public class GetOrdersByCustomerIdPagedQuery : IRequest<IEnumerable<OrderResponse>>
{
    public GetOrdersByCustomerIdPagedQuery(int customerId)
    {
        CustomerId = customerId;
    }

    public int CustomerId { get; }
    public PagingParameters PagingParameters { get; set; }
}

public class GetOrdersByCustomerIdPagedQueryHandler : IRequestHandler<GetOrdersByCustomerIdPagedQuery,
    IEnumerable<OrderResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByCustomerIdPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByCustomerIdPagedQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.OrderRepository.GetByConditionPagedAsync(o => o.CustomerId == request.CustomerId,
            request.PagingParameters, false, cancellationToken);
        
        return _mapper.Map<IEnumerable<OrderResponse>>(orders);
    }
}