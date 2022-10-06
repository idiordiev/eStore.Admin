using System;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Delete
{
    public class DeleteGamepadCommandHandler : IRequestHandler<DeleteGamepadCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGamepadCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteGamepadCommand request, CancellationToken cancellationToken)
        {
            var gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, false, cancellationToken);
            if (gamepad is null)
                return false;

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation of deleting gamepad has been cancelled.");

            _unitOfWork.GamepadRepository.Delete(gamepad);
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}