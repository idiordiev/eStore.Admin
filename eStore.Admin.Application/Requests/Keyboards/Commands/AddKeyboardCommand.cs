using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.Keyboards.Commands;

public class AddKeyboardCommand : IRequest<KeyboardResponse>
{
    public KeyboardDto Keyboard { get; set; }
}

public class AddKeyboardCommandHandler : IRequestHandler<AddKeyboardCommand, KeyboardResponse>
{
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddKeyboardCommandHandler> _logger;

    public AddKeyboardCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IClock clock,
        ILogger<AddKeyboardCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<KeyboardResponse> Handle(AddKeyboardCommand request, CancellationToken cancellationToken)
    {
        var keyboard = _mapper.Map<Keyboard>(request.Keyboard);
        keyboard.Created = _clock.UtcNow();
        keyboard.LastModified = _clock.UtcNow();

        _unitOfWork.KeyboardRepository.Add(keyboard);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The keyboard with id {KeyboardId} has been added", keyboard.Id);

        return _mapper.Map<KeyboardResponse>(keyboard);
    }
}