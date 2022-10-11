using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Add
{
    public class AddKeyboardCommand : IRequest<KeyboardResponse>
    {
        public KeyboardDto Keyboard { get; set; }
    }
}