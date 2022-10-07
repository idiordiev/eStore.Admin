using System;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.SetAsPresent
{
    public class SetKeyboardAsPresentCommandHandler : IRequestHandler<SetKeyboardAsPresentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetKeyboardAsPresentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SetKeyboardAsPresentCommand request, CancellationToken cancellationToken)
        {
            var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true, cancellationToken);
            if (keyboard is null)
                return false;

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation has been cancelled.");
            
            keyboard.IsDeleted = false;
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}