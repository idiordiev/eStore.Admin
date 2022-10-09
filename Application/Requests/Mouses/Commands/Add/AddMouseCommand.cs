using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Commands.Add
{
    public class AddMouseCommand : IRequest<MouseResponse>
    {
        public MouseRequest Mouse { get; set; }
    }
}