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
            var predicate = _filterExpressionFactory.CreateExpression(request.FilterModel);
            var gamepads = await _unitOfWork.GamepadRepository.GetByConditionPagedAsync(predicate, request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<GamepadResponse>>(gamepads);
        }
    }
}