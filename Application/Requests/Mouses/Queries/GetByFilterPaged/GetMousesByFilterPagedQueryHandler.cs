using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries.GetByFilterPaged
{
    public class GetMousesByFilterPagedQueryHandler : IRequestHandler<GetMousesByFilterPagedQuery, IEnumerable<MouseResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMouseFilterExpressionFactory _filterExpressionFactory;

        public GetMousesByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMouseFilterExpressionFactory filterExpressionFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterExpressionFactory = filterExpressionFactory;
        }

        public async Task<IEnumerable<MouseResponse>> Handle(GetMousesByFilterPagedQuery request, CancellationToken cancellationToken)
        {
            var predicate = _filterExpressionFactory.CreateExpression(request.FilterModel);
            var mouses = await _unitOfWork.MouseRepository.GetByConditionPagedAsync(predicate, request.PagingParameters, false,
                cancellationToken);
            return _mapper.Map<IEnumerable<MouseResponse>>(mouses);
        }
    }
}