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

namespace eStore_Admin.Application.Requests.Mousepads.Queries
{
    public class GetMousepadsByFilterPagedQuery : IRequest<IEnumerable<MousepadResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
        public MousepadFilterModel FilterModel { get; set; }
    }

    public class GetMousepadsByFilterPagedQueryHandler : IRequestHandler<GetMousepadsByFilterPagedQuery,
            IEnumerable<MousepadResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPredicateFactory<Mousepad, MousepadFilterModel> _predicateFactory;
        private readonly IUnitOfWork _unitOfWork;

        public GetMousepadsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IPredicateFactory<Mousepad, MousepadFilterModel> predicateFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<MousepadResponse>> Handle(GetMousepadsByFilterPagedQuery request,
            CancellationToken cancellationToken)
        {
            var predicate = _predicateFactory.CreateExpression(request.FilterModel);
            var mousepads =
                await _unitOfWork.MousepadRepository.GetByConditionPagedAsync(predicate, request.PagingParameters,
                    false,
                    cancellationToken);
            return _mapper.Map<IEnumerable<MousepadResponse>>(mousepads);
        }
    }
}