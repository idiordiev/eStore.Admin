using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetByFilterPaged
{
    public class GetGamepadsByFilterPagedQueryHandler : IRequestHandler<GetGamepadsByFilterPagedQuery, IEnumerable<GamepadResponse>>
    {
        private readonly IGamepadFilterExpressionFactory _filterExpressionFactory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetGamepadsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IGamepadFilterExpressionFactory filterExpressionFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterExpressionFactory = filterExpressionFactory;
        }

        public async Task<IEnumerable<GamepadResponse>> Handle(GetGamepadsByFilterPagedQuery request, CancellationToken cancellationToken)
        {
            var pagingParams = new PagingParameters(request.PageSize, request.PageNumber);
            var filterModel = _mapper.Map<GamepadFilterModel>(request);
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The query has been cancelled.");

            var predicate = _filterExpressionFactory.CreateExpression(filterModel);
            var gamepads = await _unitOfWork.GamepadRepository.GetByConditionPagedAsync(predicate, pagingParams, false, cancellationToken);
            return _mapper.Map<IEnumerable<GamepadResponse>>(gamepads);
        }
    }
}