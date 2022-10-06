using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Add
{
    public class AddGamepadCommand : IRequest<GamepadResponse>
    {
        public GamepadRequest Gamepad { get; set; }
    }
}