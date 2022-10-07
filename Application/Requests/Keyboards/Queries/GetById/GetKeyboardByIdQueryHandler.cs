using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetById
{
    public class GetKeyboardByIdQueryHandler : IRequestHandler<GetKeyboardByIdQuery, KeyboardResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetKeyboardByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<KeyboardResponse> Handle(GetKeyboardByIdQuery request, CancellationToken cancellationToken)
        {
            var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, false, cancellationToken);
            return _mapper.Map<KeyboardResponse>(keyboard);
        }
    }
}