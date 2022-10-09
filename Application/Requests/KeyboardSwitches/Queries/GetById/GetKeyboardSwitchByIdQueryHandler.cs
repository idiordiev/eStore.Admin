using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Queries.GetById
{
    public class GetKeyboardSwitchByIdQueryHandler : IRequestHandler<GetKeyboardSwitchByIdQuery, KeyboardSwitchResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetKeyboardSwitchByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<KeyboardSwitchResponse> Handle(GetKeyboardSwitchByIdQuery request, CancellationToken cancellationToken)
        {
            var keyboardSwitch = await _unitOfWork.KeyboardSwitchRepository.GetByIdAsync(request.SwitchId, false, cancellationToken);
            return _mapper.Map<KeyboardSwitchResponse>(keyboardSwitch);
        }
    }
}