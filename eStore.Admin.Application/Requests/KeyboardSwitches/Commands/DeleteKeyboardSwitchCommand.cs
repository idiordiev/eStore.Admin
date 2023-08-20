using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.KeyboardSwitches.Commands;

public class DeleteKeyboardSwitchCommand : IRequest<bool>
{
    public DeleteKeyboardSwitchCommand(int switchId)
    {
        SwitchId = switchId;
    }

    public int SwitchId { get; }
}

public class DeleteKeyboardSwitchCommandHandler : IRequestHandler<DeleteKeyboardSwitchCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteKeyboardSwitchCommandHandler> _logger;

    public DeleteKeyboardSwitchCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteKeyboardSwitchCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteKeyboardSwitchCommand request, CancellationToken cancellationToken)
    {
        var keyboardSwitch = await _unitOfWork.KeyboardSwitchRepository.GetByIdAsync(request.SwitchId, true,
            cancellationToken);
        if (keyboardSwitch is null)
        {
            _logger.LogInformation("The keyboard switch with id {KeyboardSwitchId} has not been found", request.SwitchId);
            return false;
        }

        _unitOfWork.KeyboardSwitchRepository.Delete(keyboardSwitch);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The keyboard switch with id {KeyboardSwitchId} has been deleted", keyboardSwitch.Id);

        return true;
    }
}