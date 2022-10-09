using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Commands.Add
{
    public class AddMousepadCommand : IRequest<MousepadResponse>
    {
        public MousepadRequest Mousepad { get; set; }
    }
}