using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.SetAsDeleted
{
    public class SetKeyboardAsDeletedCommand : IRequest<bool>
    {
        public SetKeyboardAsDeletedCommand(int keyboardId)
        {
            KeyboardId = keyboardId;
        }

        public int KeyboardId { get; }
    }
}