using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Commands.Delete
{
    public class DeleteMouseCommandHandler : IRequestHandler<DeleteMouseCommand, bool>
    {
        private readonly ILoggingService _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMouseCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteMouseCommand request, CancellationToken cancellationToken)
        {
            Mouse mouse = await _unitOfWork.MouseRepository.GetByIdAsync(request.MouseId, true, cancellationToken);
            if (mouse is null)
            {
                _logger.LogInformation("The mouse with id {0} has not been found.", request.MouseId);
                return false;
            }

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.MouseRepository.Delete(mouse);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The mouse with id {0} has been deleted.", mouse.Id);

            return true;
        }
    }
}