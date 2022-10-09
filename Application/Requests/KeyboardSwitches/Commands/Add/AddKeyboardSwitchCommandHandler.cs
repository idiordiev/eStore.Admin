using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Add
{
    public class AddKeyboardSwitchCommandHandler : IRequestHandler<AddKeyboardSwitchCommand, KeyboardSwitchResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggingService _logger;

        public AddKeyboardSwitchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<KeyboardSwitchResponse> Handle(AddKeyboardSwitchCommand request, CancellationToken cancellationToken)
        {
            var keyboardSwitch = _mapper.Map<KeyboardSwitch>(request.KeyboardSwitch);
            
            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.KeyboardSwitchRepository.Add(keyboardSwitch);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            _logger.LogInformation("The keyboard switch with id {0} has been added.", keyboardSwitch.Id);

            return _mapper.Map<KeyboardSwitchResponse>(keyboardSwitch);
        }
    }
}