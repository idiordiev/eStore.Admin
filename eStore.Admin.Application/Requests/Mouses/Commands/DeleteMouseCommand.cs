using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Mouses.Commands;

public class DeleteMouseCommand : IRequest<bool>
{
    public DeleteMouseCommand(int mouseId)
    {
        MouseId = mouseId;
    }

    public int MouseId { get; }
}

public class DeleteMouseCommandHandler : IRequestHandler<DeleteMouseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMouseCommandHandler> _logger;

    public DeleteMouseCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteMouseCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMouseCommand request, CancellationToken cancellationToken)
    {
        var mouse = await _unitOfWork.MouseRepository.GetByIdAsync(request.MouseId, true, cancellationToken);
        if (mouse is null)
        {
            _logger.LogInformation("The mouse with id {MouseId} has not been found", request.MouseId);
            return false;
        }

        _unitOfWork.MouseRepository.Delete(mouse);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The mouse with id {MouseId} has been deleted", mouse.Id);

        return true;
    }
}