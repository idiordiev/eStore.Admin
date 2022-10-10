using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries.GetAllPaged
{
    public class GetAllOrdersPagedQueryHandler : IRequestHandler<GetAllOrdersPagedQuery, IEnumerable<OrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetAllOrdersPagedQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllPagedAsync(request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}