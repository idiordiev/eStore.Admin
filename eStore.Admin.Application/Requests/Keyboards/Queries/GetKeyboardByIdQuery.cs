using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using MediatR;

namespace eStore.Admin.Application.Requests.Keyboards.Queries;

public class GetKeyboardByIdQuery : IRequest<KeyboardResponse>
{
    public GetKeyboardByIdQuery(int keyboardId)
    {
        KeyboardId = keyboardId;
    }

    public int KeyboardId { get; }
}

public class GetKeyboardByIdQueryHandler : IRequestHandler<GetKeyboardByIdQuery, KeyboardResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetKeyboardByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<KeyboardResponse> Handle(GetKeyboardByIdQuery request, CancellationToken cancellationToken)
    {
        var keyboard = await _unitOfWork.KeyboardRepository.GetByIdAsync(request.KeyboardId, false,
            cancellationToken);
        
        return _mapper.Map<KeyboardResponse>(keyboard);
    }
}