using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Commands.Add
{
    public class AddMouseCommand : IRequest<MouseResponse>
    {
        public MouseDto Mouse { get; set; }
    }
}