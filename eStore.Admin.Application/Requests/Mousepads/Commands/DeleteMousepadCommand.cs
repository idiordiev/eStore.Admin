using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Mousepads.Commands;

public class DeleteMousepadCommand : IRequest<bool>
{
    public DeleteMousepadCommand(int mousepadId)
    {
        MousepadId = mousepadId;
    }

    public int MousepadId { get; }
}

public class DeleteMousepadCommandHandler : IRequestHandler<DeleteMousepadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMousepadCommandHandler> _logger;

    public DeleteMousepadCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteMousepadCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMousepadCommand request, CancellationToken cancellationToken)
    {
        var mousepad = await _unitOfWork.MousepadRepository.GetByIdAsync(request.MousepadId, true,
            cancellationToken);
        if (mousepad is null)
        {
            _logger.LogInformation("The mousepad with id {MousepadId} has not been found", request.MousepadId);
            return false;
        }

        _unitOfWork.MousepadRepository.Delete(mousepad);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The mousepad with id {MousepadId} has been deleted", mousepad.Id);

        return true;
    }
}