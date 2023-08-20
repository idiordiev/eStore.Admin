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

namespace eStore.Admin.Application.Requests.Mouses.Commands;

public class AddMouseCommand : IRequest<MouseResponse>
{
    public MouseDto Mouse { get; set; }
}

public class AddMouseCommandHandler : IRequestHandler<AddMouseCommand, MouseResponse>
{
    private readonly IClock _clock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddMouseCommandHandler> _logger;

    public AddMouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClock clock, ILogger<AddMouseCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clock = clock;
        _logger = logger;
    }

    public async Task<MouseResponse> Handle(AddMouseCommand request, CancellationToken cancellationToken)
    {
        var mouse = _mapper.Map<Mouse>(request.Mouse);
        mouse.Created = _clock.UtcNow();
        mouse.LastModified = _clock.UtcNow();

        _unitOfWork.MouseRepository.Add(mouse);
        await _unitOfWork.SaveAsync(cancellationToken);

        _logger.LogInformation("The mouse with id {MouseId} has been added", mouse.Id);

        return _mapper.Map<MouseResponse>(mouse);
    }
}