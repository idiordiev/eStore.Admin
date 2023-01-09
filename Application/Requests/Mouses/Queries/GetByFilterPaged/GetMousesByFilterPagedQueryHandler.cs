using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries.GetByFilterPaged
{
    public class
        GetMousesByFilterPagedQueryHandler : IRequestHandler<GetMousesByFilterPagedQuery, IEnumerable<MouseResponse>>
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
                false,
                cancellationToken);
            return _mapper.Map<IEnumerable<MouseResponse>>(mouses);
        }
    }
}