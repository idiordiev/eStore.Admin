using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Edit
{
    public class EditGamepadCommandHandler : IRequestHandler<EditGamepadCommand, GamepadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditGamepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GamepadResponse> Handle(EditGamepadCommand request, CancellationToken cancellationToken)
        {
            var gamepad = await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, true, cancellationToken);
            if (gamepad is null)
                throw new KeyNotFoundException($"The gamepad with the id {request.GamepadId} has not been found.");

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation of editing gamepad has been cancelled.");
            _mapper.Map(gamepad, request.Gamepad);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<GamepadResponse>(gamepad);
        }
    }
}