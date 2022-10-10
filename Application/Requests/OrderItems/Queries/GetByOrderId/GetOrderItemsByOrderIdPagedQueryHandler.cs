using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Queries.GetByOrderId
{
    public class GetOrderItemsByOrderIdPagedQueryHandler : IRequestHandler<GetOrderItemsByOrderIdPagedQuery, IEnumerable<OrderItemResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderItemsByOrderIdPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemResponse>> Handle(GetOrderItemsByOrderIdPagedQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await _unitOfWork.OrderItemRepository.GetByConditionPagedAsync(oi => oi.OrderId == request.OrderId,
                request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
        }
    }
}