using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Edit
{
    public class EditGamepadCommandHandler : IRequestHandler<EditGamepadCommand, GamepadResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditGamepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<GamepadResponse> Handle(EditGamepadCommand request, CancellationToken cancellationToken)
        {
            Gamepad gamepad =
                await _unitOfWork.GamepadRepository.GetByIdAsync(request.GamepadId, true, cancellationToken);
            if (gamepad is null)
            {
                throw new KeyNotFoundException($"The gamepad with the id {request.GamepadId} has not been found.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            _mapper.Map(gamepad, request.Gamepad);
            gamepad.LastModified = _dateTimeService.Now();
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The gamepad with id {0} has been updated.", gamepad.Id);

            return _mapper.Map<GamepadResponse>(gamepad);
        }
    }
}