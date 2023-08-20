using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;

namespace eStore.Admin.Application.Requests.Keyboards.Queries;

public class GetAllKeyboardsPagedQuery : IRequest<IEnumerable<KeyboardResponse>>
{
    public PagingParameters PagingParameters { get; set; }
}

public class GetAllKeyboardsPagedQueryHandler : IRequestHandler<GetAllKeyboardsPagedQuery,
    IEnumerable<KeyboardResponse>>
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
        var keyboards = await _unitOfWork.KeyboardRepository.GetAllPagedAsync(request.PagingParameters, false,
            cancellationToken);
        
        return _mapper.Map<IEnumerable<KeyboardResponse>>(keyboards);
    }
}