using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetById
{
    public class GetGamepadByIdQueryHandler : IRequestHandler<GetGamepadByIdQuery, GamepadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetGamepadByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GamepadResponse> Handle(GetGamepadByIdQuery request, CancellationToken cancellationToken)
        {
            var gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, false, cancellationToken);
            return _mapper.Map<GamepadResponse>(gamepad);
        }
    }
}