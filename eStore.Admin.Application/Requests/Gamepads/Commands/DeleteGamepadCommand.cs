using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Gamepads.Commands;

public class DeleteGamepadCommand : IRequest<bool>
{
    public DeleteGamepadCommand(int gamepadId)
    {
        GamepadId = gamepadId;
    }

    public int GamepadId { get; }
}

public class DeleteGamepadCommandHandler : IRequestHandler<DeleteGamepadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteGamepadCommandHandler> _logger;

    public DeleteGamepadCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteGamepadCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGamepadCommand request, CancellationToken cancellationToken)
    {
        var gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, false, cancellationToken);
        if (gamepad is null)
        {
            _logger.LogInformation("The gamepad with id {GamepadId} has not been found", request.GamepadId);
            return false;
        }

        _unitOfWork.GamepadRepository.Delete(gamepad);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The gamepad with id {GamepadId} has been deleted", gamepad.Id);

        return true;
    }
}