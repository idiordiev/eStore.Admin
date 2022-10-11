using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Commands.Edit
{
    public class EditMouseCommand : IRequest<MouseResponse>
    {
        public EditMouseCommand(int mouseId)
        {
            MouseId = mouseId;
        }

        public int MouseId { get; }
        public MouseDto Mouse { get; set; }
    }
}