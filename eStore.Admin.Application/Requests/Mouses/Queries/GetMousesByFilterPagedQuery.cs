using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;
using MediatR;

namespace eStore.Admin.Application.Requests.Mouses.Queries;

public class GetMousesByFilterPagedQuery : IRequest<IEnumerable<MouseResponse>>
{
    public PagingParameters PagingParameters { get; set; }
    public MouseFilterModel FilterModel { get; set; }
}

public class GetMousesByFilterPagedQueryHandler : IRequestHandler<GetMousesByFilterPagedQuery,
    IEnumerable<MouseResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPredicateFactory<Mouse, MouseFilterModel> _predicateFactory;
    private readonly IUnitOfWork _unitOfWork;

    public GetMousesByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
        IPredicateFactory<Mouse, MouseFilterModel> predicateFactory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _predicateFactory = predicateFactory;
    }

    public async Task<IEnumerable<MouseResponse>> Handle(GetMousesByFilterPagedQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = _predicateFactory.CreateExpression(request.FilterModel);
        var mouses = await _unitOfWork.MouseRepository.GetByConditionPagedAsync(predicate, request.PagingParameters,
            false, cancellationToken);
        return _mapper.Map<IEnumerable<MouseResponse>>(mouses);
    }
}