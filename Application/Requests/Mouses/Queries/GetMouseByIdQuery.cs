using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries
{
    public class GetMouseByIdQuery : IRequest<MouseResponse>
    {
        public GetMouseByIdQuery(int mouseId)
        {
            MouseId = mouseId;
        }

        public int MouseId { get; }
    }

    public class GetMouseByIdQueryHandler : IRequestHandler<GetMouseByIdQuery, MouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetMouseByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MouseResponse> Handle(GetMouseByIdQuery request, CancellationToken cancellationToken)
        {
            Mouse mouse = await _unitOfWork.MouseRepository.GetByIdAsync(request.MouseId, false, cancellationToken);
            return _mapper.Map<MouseResponse>(mouse);
        }
    }
}