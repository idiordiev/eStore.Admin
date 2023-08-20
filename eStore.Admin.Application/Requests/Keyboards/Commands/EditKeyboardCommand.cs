using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Keyboards.Commands;

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
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditKeyboardCommandHandler> _logger;

    public EditKeyboardCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IClock clock, ILogger<EditKeyboardCommandHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _clock = clock;
        _logger = logger;
    }

    public async Task<KeyboardResponse> Handle(EditKeyboardCommand request, CancellationToken cancellationToken)
    {
        var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, true,
            cancellationToken);
        if (keyboard is null)
        {
            throw new KeyNotFoundException($"The keyboard with the id {request.KeyboardId} has not been found.");
        }

        _mapper.Map(request.Keyboard, keyboard);
        keyboard.LastModified = _clock.UtcNow();
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The keyboard with id {KeyboardId} has been deleted", keyboard.Id);

        return _mapper.Map<KeyboardResponse>(keyboard);
    }
}