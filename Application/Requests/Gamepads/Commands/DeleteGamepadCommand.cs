using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands;

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
    private readonly ILoggingService _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGamepadCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGamepadCommand request, CancellationToken cancellationToken)
    {
        Gamepad gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, false, cancellationToken);
        if (gamepad is null)
        {
            _logger.LogInformation("The gamepad with id {0} has not been found.", request.GamepadId);
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();

        _unitOfWork.GamepadRepository.Delete(gamepad);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The gamepad with id {0} has been deleted.", gamepad.Id);

        return true;
    }
}