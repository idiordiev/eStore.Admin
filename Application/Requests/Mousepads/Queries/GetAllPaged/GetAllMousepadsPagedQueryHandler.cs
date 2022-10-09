using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Queries.GetAllPaged
{
    public class GetAllMousepadsPagedQueryHandler : IRequestHandler<GetAllMousepadPagedQuery, IEnumerable<MousepadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMousepadsPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MousepadResponse>> Handle(GetAllMousepadPagedQuery request, CancellationToken cancellationToken)
        {
            var mousepads = await _unitOfWork.MousepadRepository.GetAllPagedAsync(request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<MousepadResponse>>(mousepads);
        }
    }
}