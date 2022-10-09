using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries.GetById
{
    public class GetMouseByIdQueryHandler : IRequestHandler<GetMouseByIdQuery, MouseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMouseByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MouseResponse> Handle(GetMouseByIdQuery request, CancellationToken cancellationToken)
        {
            var mouse = await _unitOfWork.MouseRepository.GetByIdAsync(request.MouseId, false, cancellationToken);
            return _mapper.Map<MouseResponse>(mouse);
        }
    }
}