using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.OrderItems.Queries.GetById
{
    public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, OrderItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderItemByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderItemResponse> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemId, false, cancellationToken);
            return _mapper.Map<OrderItemResponse>(orderItem);
        }
    }
}