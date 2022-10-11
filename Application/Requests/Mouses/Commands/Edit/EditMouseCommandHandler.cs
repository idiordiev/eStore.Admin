using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Commands.Edit
{
    public class EditMouseCommandHandler : IRequestHandler<EditMouseCommand, MouseResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditMouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<MouseResponse> Handle(EditMouseCommand request, CancellationToken cancellationToken)
        {
            var mouse = await _unitOfWork.MouseRepository.GetByIdAsync(request.MouseId, true, cancellationToken);
            if (mouse is null)
                throw new KeyNotFoundException($"The mouse with the id {request.MouseId} has not been found.");

            cancellationToken.ThrowIfCancellationRequested();

            _mapper.Map(request.Mouse, mouse);
            mouse.LastModified = _dateTimeService.Now();
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The mouse with id {0} has been deleted.", mouse.Id);

            return _mapper.Map<MouseResponse>(mouse);
        }
    }
}