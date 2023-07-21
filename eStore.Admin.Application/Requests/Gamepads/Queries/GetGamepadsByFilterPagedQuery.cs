using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Gamepads.Queries;

public class GetGamepadsByFilterPagedQuery : IRequest<IEnumerable<GamepadResponse>>
{
    public PagingParameters PagingParameters { get; set; }
    public GamepadFilterModel FilterModel { get; set; }
}

public class GetGamepadsByFilterPagedQueryHandler : IRequestHandler<GetGamepadsByFilterPagedQuery,
    IEnumerable<GamepadResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetGamepadsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GamepadResponse>> Handle(GetGamepadsByFilterPagedQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = request.FilterModel.CreateExpression();
        var gamepads = await _unitOfWork.GamepadRepository.GetByConditionPagedAsync(predicate, request.PagingParameters,
            false, cancellationToken);
        
        return _mapper.Map<IEnumerable<GamepadResponse>>(gamepads);
    }
}