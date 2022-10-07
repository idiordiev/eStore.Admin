using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Add
{
    public class AddKeyboardCommandHandler : IRequestHandler<AddKeyboardCommand, KeyboardResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddKeyboardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<KeyboardResponse> Handle(AddKeyboardCommand request, CancellationToken cancellationToken)
        {
            var keyboard = _mapper.Map<Keyboard>(request.Keyboard);
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("The operation has been cancelled.");

            _unitOfWork.KeyboardRepository.Add(keyboard);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<KeyboardResponse>(keyboard);
        }
    }
}