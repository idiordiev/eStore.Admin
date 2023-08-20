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

namespace eStore.Admin.Application.Requests.Mouses.Commands;

public class EditMouseCommand : IRequest<MouseResponse>
{
    public EditMouseCommand(int mouseId)
    {
        MouseId = mouseId;
    }

    public int MouseId { get; }
    public MouseDto Mouse { get; set; }
}

public class EditMouseCommandHandler : IRequestHandler<EditMouseCommand, MouseResponse>
{
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditMouseCommandHandler> _logger;

    public EditMouseCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IClock clock, ILogger<EditMouseCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<MouseResponse> Handle(EditMouseCommand request, CancellationToken cancellationToken)
    {
        var mouse = await _unitOfWork.MouseRepository.GetByIdAsync(request.MouseId, true, cancellationToken);
        if (mouse is null)
        {
            throw new KeyNotFoundException($"The mouse with the id {request.MouseId} has not been found.");
        }

        _mapper.Map(request.Mouse, mouse);
        mouse.LastModified = _clock.UtcNow();
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The mouse with id {MouseId} has been deleted", mouse.Id);

        return _mapper.Map<MouseResponse>(mouse);
    }
}