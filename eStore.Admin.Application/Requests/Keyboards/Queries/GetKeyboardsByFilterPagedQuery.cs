﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Keyboards.Queries;

public class GetKeyboardsByFilterPagedQuery : IRequest<IEnumerable<KeyboardResponse>>
{
    public PagingParameters PagingParameters { get; set; }
    public KeyboardFilterModel FilterModel { get; set; }
}

public class GetKeyboardsByFilterPagedQueryHandler : IRequestHandler<GetKeyboardsByFilterPagedQuery,
    IEnumerable<KeyboardResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetKeyboardsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<KeyboardResponse>> Handle(GetKeyboardsByFilterPagedQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = request.FilterModel.CreateExpression();
        var keyboards = await _unitOfWork.KeyboardRepository.GetByConditionPagedAsync(predicate,
            request.PagingParameters, false, cancellationToken);
        
        return _mapper.Map<IEnumerable<KeyboardResponse>>(keyboards);
    }
}