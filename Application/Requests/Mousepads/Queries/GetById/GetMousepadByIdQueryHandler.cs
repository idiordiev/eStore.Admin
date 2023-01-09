using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Queries.GetById
{
    public class GetMousepadByIdQueryHandler : IRequestHandler<GetMousepadByIdQuery, MousepadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetMousepadByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MousepadResponse> Handle(GetMousepadByIdQuery request, CancellationToken cancellationToken)
        {
            Mousepad mousepad =
                await _unitOfWork.MousepadRepository.GetByIdAsync(request.MousepadId, false, cancellationToken);
            return _mapper.Map<MousepadResponse>(mousepad);
        }
    }
}