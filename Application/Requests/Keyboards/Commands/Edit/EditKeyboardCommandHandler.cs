using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Edit
{
    public class EditKeyboardCommandHandler : IRequestHandler<EditKeyboardCommand, KeyboardResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditKeyboardCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<KeyboardResponse> Handle(EditKeyboardCommand request, CancellationToken cancellationToken)
        {
            var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true, cancellationToken);
            if (keyboard is null)
                throw new KeyNotFoundException($"The keyboard with the id {request.KeyboardId} has not been found.");

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation has been cancelled.");

            _mapper.Map(request.Keyboard, keyboard);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<KeyboardResponse>(keyboard);
        }
    }
}