using System;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Delete
{
    public class DeleteKeyboardCommandHandler : IRequestHandler<DeleteKeyboardCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggingService _logger;

        public DeleteKeyboardCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteKeyboardCommand request, CancellationToken cancellationToken)
        {
            var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true, cancellationToken);
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