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

namespace eStore.Admin.Application.Requests.Mousepads.Commands;

public class EditMousepadCommand : IRequest<MousepadResponse>
{
    public EditMousepadCommand(int mousepadId)
    {
        MousepadId = mousepadId;
    }

    public int MousepadId { get; }
    public MousepadDto Mousepad { get; set; }
}

public class EditMousepadCommandHandler : IRequestHandler<EditMousepadCommand, MousepadResponse>
{
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditMousepadCommandHandler> _logger;

    public EditMousepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClock clock, ILogger<EditMousepadCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<MousepadResponse> Handle(EditMousepadCommand request, CancellationToken cancellationToken)
    {
        var mousepad = await _unitOfWork.MousepadRepository.GetByIdAsync(request.MousepadId, true,
            cancellationToken);
        if (mousepad is null)
        {
            throw new KeyNotFoundException($"The mousepad with the id {request.MousepadId} has not been found.");
        }

        _mapper.Map(request.Mousepad, mousepad);
        mousepad.LastModified = _clock.UtcNow();
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The mousepad with id {MousepadId} has been deleted", mousepad.Id);

        return _mapper.Map<MousepadResponse>(mousepad);
    }
}