using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Commands.Add
{
    public class AddMousepadCommand : IRequest<MousepadResponse>
    {
        public MousepadDto Mousepad { get; set; }
    }
}