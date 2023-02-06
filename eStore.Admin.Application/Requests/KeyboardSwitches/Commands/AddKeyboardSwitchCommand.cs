using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Interfaces.Services;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;
using MediatR;

namespace eStore.Admin.Application.Requests.KeyboardSwitches.Commands;

public class AddKeyboardSwitchCommand : IRequest<KeyboardSwitchResponse>
{
    public KeyboardSwitchDto KeyboardSwitch { get; set; }
}

public class AddKeyboardSwitchCommandHandler : IRequestHandler<AddKeyboardSwitchCommand, KeyboardSwitchResponse>
{
    private readonly ILoggingService _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddKeyboardSwitchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<KeyboardSwitchResponse> Handle(AddKeyboardSwitchCommand request,
        CancellationToken cancellationToken)
    {
        var keyboardSwitch = _mapper.Map<KeyboardSwitch>(request.KeyboardSwitch);

        cancellationToken.ThrowIfCancellationRequested();

        _unitOfWork.KeyboardSwitchRepository.Add(keyboardSwitch);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The keyboard switch with id {0} has been added.", keyboardSwitch.Id);

        return _mapper.Map<KeyboardSwitchResponse>(keyboardSwitch);
    }
}