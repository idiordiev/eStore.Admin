using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Mousepads.Queries;

public class GetMousepadsByFilterPagedQuery : IRequest<IEnumerable<MousepadResponse>>
{
    public PagingParameters PagingParameters { get; set; }
    public MousepadFilterModel FilterModel { get; set; }
}

public class GetMousepadsByFilterPagedQueryHandler : IRequestHandler<GetMousepadsByFilterPagedQuery,
    IEnumerable<MousepadResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetMousepadsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MousepadResponse>> Handle(GetMousepadsByFilterPagedQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = request.FilterModel.CreateExpression();
        var mousepads = await _unitOfWork.MousepadRepository.GetByConditionPagedAsync(predicate,
            request.PagingParameters, false, cancellationToken);
        
        return _mapper.Map<IEnumerable<MousepadResponse>>(mousepads);
    }
}