using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetAllPaged
{
    public class
        GetAllKeyboardsPagedQueryHandler : IRequestHandler<GetAllKeyboardsPagedQuery, IEnumerable<KeyboardResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllKeyboardsPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KeyboardResponse>> Handle(GetAllKeyboardsPagedQuery request,
            CancellationToken cancellationToken)
        {
            var keyboards =
                await _unitOfWork.KeyboardRepository.GetAllPagedAsync(request.PagingParameters, false,
                    cancellationToken);
            return _mapper.Map<IEnumerable<KeyboardResponse>>(keyboards);
        }
    }
}