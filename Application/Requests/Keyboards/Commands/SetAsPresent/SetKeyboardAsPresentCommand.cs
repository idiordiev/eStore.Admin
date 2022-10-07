using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.SetAsPresent
{
    public class SetKeyboardAsPresentCommand : IRequest<bool>
    {
        public SetKeyboardAsPresentCommand(int keyboardId)
        {
            KeyboardId = keyboardId;
        }

        public int KeyboardId { get; }
    }
}