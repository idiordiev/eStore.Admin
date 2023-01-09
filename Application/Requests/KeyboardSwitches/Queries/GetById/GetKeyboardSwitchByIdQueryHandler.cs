using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Queries.GetById
{
    public class GetKeyboardSwitchByIdQueryHandler : IRequestHandler<GetKeyboardSwitchByIdQuery, KeyboardSwitchResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetKeyboardSwitchByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<KeyboardSwitchResponse> Handle(GetKeyboardSwitchByIdQuery request,
            CancellationToken cancellationToken)
        {
            KeyboardSwitch keyboardSwitch =
                await _unitOfWork.KeyboardSwitchRepository.GetByIdAsync(request.SwitchId, false, cancellationToken);
            return _mapper.Map<KeyboardSwitchResponse>(keyboardSwitch);
        }
    }
}