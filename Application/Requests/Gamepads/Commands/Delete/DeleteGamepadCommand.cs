using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Delete
{
    public class DeleteGamepadCommand : IRequest<bool>
    {
        public DeleteGamepadCommand(int gamepadId)
        {
            GamepadId = gamepadId;
        }

        public int GamepadId { get; }
    }
}