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

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetByFilterPaged
{
    public class GetKeyboardsByFilterPagedQueryHandler : IRequestHandler<GetKeyboardsByFilterPagedQuery, IEnumerable<KeyboardResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IKeyboardFilterExpressionFactory _filterExpressionFactory;

        public GetKeyboardsByFilterPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IKeyboardFilterExpressionFactory filterExpressionFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterExpressionFactory = filterExpressionFactory;
        }

        public async Task<IEnumerable<KeyboardResponse>> Handle(GetKeyboardsByFilterPagedQuery request, CancellationToken cancellationToken)
        {
            var predicate = _filterExpressionFactory.CreateExpression(request.FilterModel);
            var keyboards =
                await _unitOfWork.KeyboardRepository.GetByConditionPagedAsync(predicate, request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<KeyboardResponse>>(keyboards);
        }
    }
}