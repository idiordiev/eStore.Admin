using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using MediatR;

namespace eStore.Admin.Application.Requests.Orders.Queries;

public class GetOrderByIdQuery : IRequest<OrderResponse>
{
    public GetOrderByIdQuery(int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, false, cancellationToken);
        
        return _mapper.Map<OrderResponse>(order);
    }
}