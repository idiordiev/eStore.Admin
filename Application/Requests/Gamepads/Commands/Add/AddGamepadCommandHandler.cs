using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Add
{
    public class AddGamepadCommandHandler : IRequestHandler<AddGamepadCommand, GamepadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddGamepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GamepadResponse> Handle(AddGamepadCommand request, CancellationToken cancellationToken)
        {
            var gamepad = _mapper.Map<Gamepad>(request.Gamepad);
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation of adding new customer has been cancelled.");

            _unitOfWork.GamepadRepository.Add(gamepad);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<GamepadResponse>(gamepad);
        }
    }
}