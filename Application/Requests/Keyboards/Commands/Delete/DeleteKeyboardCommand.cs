using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Delete
{
    public class DeleteKeyboardCommand : IRequest<bool>
    {
        public DeleteKeyboardCommand(int keyboardId)
        {
            KeyboardId = keyboardId;
        }

        public int KeyboardId { get; }
    }
}