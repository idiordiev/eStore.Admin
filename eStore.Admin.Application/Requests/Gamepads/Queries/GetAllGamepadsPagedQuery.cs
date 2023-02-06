using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Gamepads.Queries;

public class GetAllGamepadsPagedQuery : IRequest<IEnumerable<GamepadResponse>>
{
    public PagingParameters PagingParameters { get; set; }
}

public class GetAllGamepadsPagedQueryHandler : IRequestHandler<GetAllGamepadsPagedQuery,
    IEnumerable<GamepadResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGamepadsPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GamepadResponse>> Handle(GetAllGamepadsPagedQuery request,
        CancellationToken cancellationToken)
    {
        var gamepads = await _unitOfWork.GamepadRepository.GetAllPagedAsync(request.PagingParameters, false,
            cancellationToken);
        return _mapper.Map<IEnumerable<GamepadResponse>>(gamepads);
    }
}