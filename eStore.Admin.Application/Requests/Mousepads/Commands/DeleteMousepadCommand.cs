using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Interfaces.Services;
using eStore.Admin.Domain.Entities;
using MediatR;

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
    private readonly ILoggingService _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMousepadCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMousepadCommand request, CancellationToken cancellationToken)
    {
        Mousepad mousepad = await _unitOfWork.MousepadRepository.GetByIdAsync(request.MousepadId, true,
            cancellationToken);
        if (mousepad is null)
        {
            _logger.LogInformation("The mousepad with id {0} has not been found.", request.MousepadId);
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();

        _unitOfWork.MousepadRepository.Delete(mousepad);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The mousepad with id {0} has been deleted.", mousepad.Id);

        return true;
    }
}