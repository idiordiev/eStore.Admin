using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries.GetByCustomerIdPaged
{
    public class GetOrdersByCustomerIdPagedQueryHandler : IRequestHandler<GetOrdersByCustomerIdPagedQuery, IEnumerable<OrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerIdPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByCustomerIdPagedQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.OrderRepository.GetByConditionPagedAsync(o => o.CustomerId == request.CustomerId,
                request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}