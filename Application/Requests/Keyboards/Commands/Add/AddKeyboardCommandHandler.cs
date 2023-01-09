using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Add
{
    public class AddKeyboardCommandHandler : IRequestHandler<AddKeyboardCommand, KeyboardResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddKeyboardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        public async Task<KeyboardResponse> Handle(AddKeyboardCommand request, CancellationToken cancellationToken)
        {
            var keyboard = _mapper.Map<Keyboard>(request.Keyboard);
            keyboard.Created = _dateTimeService.Now();
            keyboard.LastModified = _dateTimeService.Now();

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.KeyboardRepository.Add(keyboard);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The keyboard with id {0} has been added.", keyboard.Id);

            return _mapper.Map<KeyboardResponse>(keyboard);
        }
    }
}