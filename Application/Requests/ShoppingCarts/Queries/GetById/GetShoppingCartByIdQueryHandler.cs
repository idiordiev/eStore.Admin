using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.ShoppingCarts.Queries.GetById
{
    public class GetShoppingCartByIdQueryHandler : IRequestHandler<GetShoppingCartByIdQuery, ShoppingCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShoppingCartByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartResponse> Handle(GetShoppingCartByIdQuery request,
            CancellationToken cancellationToken)
        {
            ShoppingCart shoppingCart =
                await _unitOfWork.ShoppingCartRepository.GetByIdWithItemsAsync(request.ShoppingCartId, false,
                    cancellationToken);
            return _mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}