using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Commands
{
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
        private readonly ILoggingService _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteKeyboardSwitchCommandHandler(IUnitOfWork unitOfWork, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteKeyboardSwitchCommand request, CancellationToken cancellationToken)
        {
            KeyboardSwitch keyboardSwitch =
                await _unitOfWork.KeyboardSwitchRepository.GetByIdAsync(request.SwitchId, true, cancellationToken);
            if (keyboardSwitch is null)
            {
                _logger.LogInformation("The keyboard switch with id {0} has not been found.", request.SwitchId);
                return false;
            }

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.KeyboardSwitchRepository.Delete(keyboardSwitch);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The keyboard switch with id {0} has been deleted.", keyboardSwitch.Id);

            return true;
        }
    }
}