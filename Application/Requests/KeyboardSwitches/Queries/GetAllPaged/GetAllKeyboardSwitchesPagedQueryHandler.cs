using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Queries.GetAllPaged
{
    public class GetAllKeyboardSwitchesPagedQueryHandler : IRequestHandler<GetAllKeyboardSwitchesPagedQuery,
        IEnumerable<KeyboardSwitchResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllKeyboardSwitchesPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KeyboardSwitchResponse>> Handle(GetAllKeyboardSwitchesPagedQuery request,
            CancellationToken cancellationToken)
        {
            var switches = await _unitOfWork.KeyboardSwitchRepository.GetAllPagedAsync(request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<KeyboardSwitchResponse>>(switches);
        }
    }
}