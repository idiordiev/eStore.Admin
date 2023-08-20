using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;
using MediatR;

namespace eStore.Admin.Application.Requests.Gamepads.Queries;

public class GetGamepadByIdQuery : IRequest<GamepadResponse>
{
    public GetGamepadByIdQuery(int gamepadId)
    {
        GamepadId = gamepadId;
    }

    public int GamepadId { get; }
}

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