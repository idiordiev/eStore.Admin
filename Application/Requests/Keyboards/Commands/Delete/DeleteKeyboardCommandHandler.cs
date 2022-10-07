using System;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Delete
{
    public class DeleteKeyboardCommandHandler : IRequestHandler<DeleteKeyboardCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteKeyboardCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteKeyboardCommand request, CancellationToken cancellationToken)
        {
            var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true, cancellationToken);
            if (keyboard is null)
                return false;

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation has been cancelled", cancellationToken);

            _unitOfWork.KeyboardRepository.Delete(keyboard);
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}