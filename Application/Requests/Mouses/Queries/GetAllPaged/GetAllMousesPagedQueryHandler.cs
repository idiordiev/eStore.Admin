using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries.GetAllPaged
{
    public class GetAllMousesPagedQueryHandler : IRequestHandler<GetAllMousesPagedQuery, IEnumerable<MouseResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMousesPagedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MouseResponse>> Handle(GetAllMousesPagedQuery request, CancellationToken cancellationToken)
        {
            var mouses = await _unitOfWork.MouseRepository.GetAllPagedAsync(request.PagingParameters, false, cancellationToken);
            return _mapper.Map<IEnumerable<MouseResponse>>(mouses);
        }
    }
}