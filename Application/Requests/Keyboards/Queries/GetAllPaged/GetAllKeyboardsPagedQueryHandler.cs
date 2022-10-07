using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetAllPaged
{
    public class GetAllKeyboardsPagedQueryHandler : IRequestHandler<GetAllKeyboardsPagedQuery, IEnumerable<KeyboardResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllKeyboardsPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KeyboardResponse>> Handle(GetAllKeyboardsPagedQuery request, CancellationToken cancellationToken)
        {
            var pagingParams = new PagingParameters(request.PageSize, request.PageNumber);
            var keyboards = await _unitOfWork.KeyboardRepository.GetAllPagedAsync(pagingParams, false, cancellationToken);
            return _mapper.Map<IEnumerable<KeyboardResponse>>(keyboards);
        }
    }
}