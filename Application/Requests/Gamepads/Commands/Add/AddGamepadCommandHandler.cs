using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Add
{
    public class AddGamepadCommandHandler : IRequestHandler<AddGamepadCommand, GamepadResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddGamepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<GamepadResponse> Handle(AddGamepadCommand request, CancellationToken cancellationToken)
        {
            var gamepad = _mapper.Map<Gamepad>(request.Gamepad);
            gamepad.Created = _dateTimeService.Now();
            gamepad.LastModified = _dateTimeService.Now();

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.GamepadRepository.Add(gamepad);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The gamepad with id {0} has been added.", gamepad.Id);

            return _mapper.Map<GamepadResponse>(gamepad);
        }
    }
}