using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Delete
{
    public class DeleteGamepadCommand : IRequest<bool>
    {
        public int GamepadId { get; set; }
    }
}