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

namespace eStore.Admin.Application.Requests.Mousepads.Commands;

public class AddMousepadCommand : IRequest<MousepadResponse>
{
    public MousepadDto Mousepad { get; set; }
}

public class AddMousepadCommandHandler : IRequestHandler<AddMousepadCommand, MousepadResponse>
{
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddMousepadCommandHandler> _logger;

    public AddMousepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClock clock, ILogger<AddMousepadCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<MousepadResponse> Handle(AddMousepadCommand request, CancellationToken cancellationToken)
    {
        var mousepad = _mapper.Map<Mousepad>(request.Mousepad);
        mousepad.Created = _clock.UtcNow();
        mousepad.LastModified = _clock.UtcNow();

        _unitOfWork.MousepadRepository.Add(mousepad);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The mousepad with id {MousepadId} has been added", mousepad.Id);

        return _mapper.Map<MousepadResponse>(mousepad);
    }
}