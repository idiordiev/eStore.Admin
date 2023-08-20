using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Keyboards.Commands;

public class DeleteKeyboardCommand : IRequest<bool>
{
    public DeleteKeyboardCommand(int keyboardId)
    {
        KeyboardId = keyboardId;
    }

    public int KeyboardId { get; }
}

public class DeleteKeyboardCommandHandler : IRequestHandler<DeleteKeyboardCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteKeyboardCommandHandler> _logger;

    public DeleteKeyboardCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteKeyboardCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteKeyboardCommand request, CancellationToken cancellationToken)
    {
        var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true,
            cancellationToken);
        if (keyboard is null)
        {
            _logger.LogInformation("The keyboard with id {KeyboardId} has not been found", request.KeyboardId);
            return false;
        }

        _unitOfWork.KeyboardRepository.Delete(keyboard);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The keyboard with id {KeyboardId} has been deleted", keyboard.Id);

        return true;
    }
}