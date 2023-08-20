using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Gamepads.Commands;

public class AddGamepadCommand : IRequest<GamepadResponse>
{
    public GamepadDto Gamepad { get; set; }
}

public class AddGamepadCommandHandler : IRequestHandler<AddGamepadCommand, GamepadResponse>
{
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddGamepadCommandHandler> _logger;

    public AddGamepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClock clock, ILogger<AddGamepadCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<GamepadResponse> Handle(AddGamepadCommand request, CancellationToken cancellationToken)
    {
        var gamepad = _mapper.Map<Gamepad>(request.Gamepad);
        gamepad.Created = _clock.UtcNow();
        gamepad.LastModified = _clock.UtcNow();

        _unitOfWork.GamepadRepository.Add(gamepad);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The gamepad with id {GamepadId} has been added", gamepad.Id);

        return _mapper.Map<GamepadResponse>(gamepad);
    }
}