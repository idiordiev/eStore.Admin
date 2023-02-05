using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Queries;

public class GetAllMousepadsPagedQuery : IRequest<IEnumerable<MousepadResponse>>
{
    public PagingParameters PagingParameters { get; set; }
}

public class GetAllMousepadsPagedQueryHandler : IRequestHandler<GetAllMousepadsPagedQuery,
    IEnumerable<MousepadResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMousepadsPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MousepadResponse>> Handle(GetAllMousepadsPagedQuery request,
        CancellationToken cancellationToken)
    {
        var mousepads = await _unitOfWork.MousepadRepository.GetAllPagedAsync(request.PagingParameters, false,
                cancellationToken);
        return _mapper.Map<IEnumerable<MousepadResponse>>(mousepads);
    }
}