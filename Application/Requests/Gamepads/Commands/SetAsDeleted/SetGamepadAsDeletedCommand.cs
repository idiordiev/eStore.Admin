using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.SetAsDeleted
{
    public class SetGamepadAsDeletedCommand : IRequest<bool>
    {
        public int GamepadId { get; set; }
    }
}