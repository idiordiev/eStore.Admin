using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Orders.Queries;

public class GetAllOrdersPagedQuery : IRequest<IEnumerable<OrderResponse>>
{
    public PagingParameters PagingParameters { get; set; }
}

public class GetAllOrdersPagedQueryHandler : IRequestHandler<GetAllOrdersPagedQuery, IEnumerable<OrderResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllOrdersPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetAllOrdersPagedQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.OrderRepository.GetAllPagedAsync(request.PagingParameters, false,
            cancellationToken);
        return _mapper.Map<IEnumerable<OrderResponse>>(orders);
    }
}