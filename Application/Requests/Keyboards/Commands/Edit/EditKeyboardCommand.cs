using eStore_Admin.Application.RequestModels;
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
        public KeyboardRequest Keyboard { get; set; }
    }
}