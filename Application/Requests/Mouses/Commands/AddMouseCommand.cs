using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Commands
{
    public class AddMouseCommand : IRequest<MouseResponse>
    {
        public MouseDto Mouse { get; set; }
    }

    public class AddMouseCommandHandler : IRequestHandler<AddMouseCommand, MouseResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddMouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggingService logger,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<MouseResponse> Handle(AddMouseCommand request, CancellationToken cancellationToken)
        {
            var mouse = _mapper.Map<Mouse>(request.Mouse);
            mouse.Created = _dateTimeService.Now();
            mouse.LastModified = _dateTimeService.Now();

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.MouseRepository.Add(mouse);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("The mouse with id {0} has been added.", mouse.Id);

            return _mapper.Map<MouseResponse>(mouse);
        }
    }
}