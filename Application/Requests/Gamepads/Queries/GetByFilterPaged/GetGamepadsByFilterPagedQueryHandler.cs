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
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetByFilterPaged
{
    public class GetGamepadsByFilterPagedQueryHandler : IRequestHandler<GetGamepadsByFilterPagedQuery, IEnumerable<GamepadResponse>>
    {
        private readonly IPredicateFactory<Gamepad, GamepadFilterModel> _predicateFactory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetGamepadsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IPredicateFactory<Gamepad, GamepadFilterModel> predicateFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<GamepadResponse>> Handle(GetGamepadsByFilterPagedQuery request, CancellationToken cancellationToken)
        {
            var predicate = _predicateFactory.CreateExpression(request.FilterModel);
            var gamepads = await _unitOfWork.GamepadRepository.GetByConditionPagedAsync(predicate, request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<GamepadResponse>>(gamepads);
        }
    }
}