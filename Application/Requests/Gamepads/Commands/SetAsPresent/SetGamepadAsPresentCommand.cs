using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.SetAsPresent
{
    public class SetGamepadAsPresentCommand : IRequest<bool>
    {
        public int GamepadId { get; set; }
    }
}