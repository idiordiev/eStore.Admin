using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands
{
    public class EditKeyboardCommand : IRequest<KeyboardResponse>
    {
        public EditKeyboardCommand(int keyboardId)
        {
            KeyboardId = keyboardId;
        }

        public int KeyboardId { get; }
        public KeyboardDto Keyboard { get; set; }
    }

    public class EditKeyboardCommandHandler : IRequestHandler<EditKeyboardCommand, KeyboardResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditKeyboardCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggingService logger,
            IDateTimeService dateTimeService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<KeyboardResponse> Handle(EditKeyboardCommand request, CancellationToken cancellationToken)
        {
            Keyboard keyboard =
                await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true, cancellationToken);
            if (keyboard is null)
            {
                throw new KeyNotFoundException($"The keyboard with the id {request.KeyboardId} has not been found.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            _mapper.Map(request.Keyboard, keyboard);
            keyboard.LastModified = _dateTimeService.Now();
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The keyboard with id {0} has been deleted.", keyboard.Id);

            return _mapper.Map<KeyboardResponse>(keyboard);
        }
    }
}