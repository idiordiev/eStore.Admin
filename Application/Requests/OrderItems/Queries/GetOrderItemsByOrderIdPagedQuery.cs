using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Queries
{
    public class GetOrderItemsByOrderIdPagedQuery : IRequest<IEnumerable<OrderItemResponse>>
    {
        public GetOrderItemsByOrderIdPagedQuery(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
        public PagingParameters PagingParameters { get; set; }
    }

    public class GetOrderItemsByOrderIdPagedQueryHandler : IRequestHandler<GetOrderItemsByOrderIdPagedQuery,
            IEnumerable<OrderItemResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderItemsByOrderIdPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemResponse>> Handle(GetOrderItemsByOrderIdPagedQuery request,
            CancellationToken cancellationToken)
        {
            var orderItems = await _unitOfWork.OrderItemRepository.GetByConditionPagedAsync(
                oi => oi.OrderId == request.OrderId,
                request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
        }
    }
}