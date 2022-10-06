using System;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.SetAsDeleted
{
    public class SetGamepadAsDeletedCommandHandler : IRequestHandler<SetGamepadAsDeletedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetGamepadAsDeletedCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SetGamepadAsDeletedCommand request, CancellationToken cancellationToken)
        {
            var gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, true, cancellationToken);
            if (gamepad is null)
                return false;

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation of editing gamepad has been cancelled.");

            gamepad.IsDeleted = true;
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}