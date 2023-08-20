using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using MediatR;

namespace eStore.Admin.Application.Requests.KeyboardSwitches.Queries;

public class GetKeyboardSwitchByIdQuery : IRequest<KeyboardSwitchResponse>
{
    public GetKeyboardSwitchByIdQuery(int switchId)
    {
        SwitchId = switchId;
    }

    public int SwitchId { get; }
}

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
        var keyboardSwitch = await _unitOfWork.KeyboardSwitchRepository.GetByIdAsync(request.SwitchId, false,
            cancellationToken);
        
        return _mapper.Map<KeyboardSwitchResponse>(keyboardSwitch);
    }
}