using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Orders.Queries.GetById
{
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
            Order order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, false, cancellationToken);
            return _mapper.Map<OrderResponse>(order);
        }
    }
}