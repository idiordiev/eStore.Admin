using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Queries.GetByFilterPaged
{
    public class GetMousepadsByFilterPagedQueryHandler : IRequestHandler<GetMousepadsByFilterPagedQuery, IEnumerable<MousepadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMousepadFilterExpressionFactory _filterExpressionFactory;

        public GetMousepadsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMousepadFilterExpressionFactory filterExpressionFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterExpressionFactory = filterExpressionFactory;
        }

        public async Task<IEnumerable<MousepadResponse>> Handle(GetMousepadsByFilterPagedQuery request, CancellationToken cancellationToken)
        {
            var predicate = _filterExpressionFactory.CreateExpression(request.FilterModel);
            var mousepads =
                await _unitOfWork.MousepadRepository.GetByConditionPagedAsync(predicate, request.PagingParameters, false,
                    cancellationToken);
            return _mapper.Map<IEnumerable<MousepadResponse>>(mousepads);
        }
    }
}