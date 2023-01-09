using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Delete
{
    public class DeleteKeyboardCommandHandler : IRequestHandler<DeleteKeyboardCommand, bool>
    {
        private readonly ILoggingService _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteKeyboardCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteKeyboardCommand request, CancellationToken cancellationToken)
        {
            Keyboard keyboard =
                await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true, cancellationToken);
            if (keyboard is null)
            {
                _logger.LogInformation("The keyboard with id {0} has not been found.", request.KeyboardId);
                return false;
            }

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.KeyboardRepository.Delete(keyboard);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The keyboard with id {0} has been deleted.", keyboard.Id);

            return true;
        }
    }
}