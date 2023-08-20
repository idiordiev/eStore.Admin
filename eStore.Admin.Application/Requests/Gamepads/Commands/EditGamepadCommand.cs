using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Gamepads.Commands;

public class EditGamepadCommand : IRequest<GamepadResponse>
{
    public EditGamepadCommand(int gamepadId)
    {
        GamepadId = gamepadId;
    }

    public int GamepadId { get; }
    public GamepadDto Gamepad { get; set; }
}

public class EditGamepadCommandHandler : IRequestHandler<EditGamepadCommand, GamepadResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClock _clock;
    private readonly ILogger<EditGamepadCommandHandler> _logger;

    public EditGamepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClock clock, ILogger<EditGamepadCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<GamepadResponse> Handle(EditGamepadCommand request, CancellationToken cancellationToken)
    {
        var gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, true, cancellationToken);
        if (gamepad is null)
        {
            throw new KeyNotFoundException($"The gamepad with the id {request.GamepadId} has not been found.");
        }

        _mapper.Map(gamepad, request.Gamepad);
        gamepad.LastModified = _clock.UtcNow();
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The gamepad with id {GamepadId} has been updated", gamepad.Id);

        return _mapper.Map<GamepadResponse>(gamepad);
    }
}