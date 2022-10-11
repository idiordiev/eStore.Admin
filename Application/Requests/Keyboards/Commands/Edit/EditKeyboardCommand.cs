using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Edit
{
    public class EditKeyboardCommand : IRequest<KeyboardResponse>
    {
        public EditKeyboardCommand(int keyboardId)
        {
            KeyboardId = keyboardId;
        }

        public int KeyboardId { get; }
        public KeyboardDto Keyboard { get; set; }
    }
}