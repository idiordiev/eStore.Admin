using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Admin.Application.Requests.KeyboardSwitches.Commands;

public class EditKeyboardSwitchCommand : IRequest<KeyboardSwitchResponse>
{
    public EditKeyboardSwitchCommand(int switchId)
    {
        SwitchId = switchId;
    }

    public int SwitchId { get; }
    public KeyboardSwitchDto KeyboardSwitch { get; set; }
}

public class EditKeyboardSwitchCommandHandler : IRequestHandler<EditKeyboardSwitchCommand, KeyboardSwitchResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditKeyboardSwitchCommandHandler> _logger;

    public EditKeyboardSwitchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EditKeyboardSwitchCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<KeyboardSwitchResponse> Handle(EditKeyboardSwitchCommand request,
        CancellationToken cancellationToken)
    {
        var keyboardSwitch =
            await _unitOfWork.KeyboardSwitchRepository.GetByIdAsync(request.SwitchId, true, cancellationToken);
        if (keyboardSwitch is null)
        {
            throw new KeyNotFoundException($"The keyboard switch with the id {request.SwitchId} has not been found.");
        }

        _mapper.Map(request.KeyboardSwitch, keyboardSwitch);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The keyboard switch with id {KeyboardSwitchId} has been deleted", keyboardSwitch.Id);

        return _mapper.Map<KeyboardSwitchResponse>(keyboardSwitch);
    }
}