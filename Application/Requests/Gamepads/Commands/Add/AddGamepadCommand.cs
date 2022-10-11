using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Add
{
    public class AddGamepadCommand : IRequest<GamepadResponse>
    {
        public GamepadDto Gamepad { get; set; }
    }
}