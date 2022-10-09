using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Commands.Edit
{
    public class EditMousepadCommandHandler : IRequestHandler<EditMousepadCommand, MousepadResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggingService _logger;
        private readonly IDateTimeService _dateTimeService;

        public EditMousepadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<MousepadResponse> Handle(EditMousepadCommand request, CancellationToken cancellationToken)
        {
            var mousepad = await _unitOfWork.MousepadRepository.GetByIdAsync(request.MousepadId, true, cancellationToken);
            if (mousepad is null)
                throw new KeyNotFoundException($"The mousepad with the id {request.MousepadId} has not been found.");

            cancellationToken.ThrowIfCancellationRequested();

            _mapper.Map(request.Mousepad, mousepad);
            mousepad.LastModified = _dateTimeService.Now();
            await _unitOfWork.SaveAsync(cancellationToken);
            
            _logger.LogInformation("The mousepad with id {0} has been deleted.", mousepad.Id);

            return _mapper.Map<MousepadResponse>(mousepad);
        }
    }
}