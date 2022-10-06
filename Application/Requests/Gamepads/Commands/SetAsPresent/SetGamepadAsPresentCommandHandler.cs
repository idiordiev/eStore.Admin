using System;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.SetAsPresent
{
    public class SetGamepadAsPresentCommandHandler : IRequestHandler<SetGamepadAsPresentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetGamepadAsPresentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SetGamepadAsPresentCommand request, CancellationToken cancellationToken)
        {
            var gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, true, cancellationToken);
            if (gamepad is null)
                return false;

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation of editing gamepad has been cancelled.");

            gamepad.IsDeleted = false;
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}