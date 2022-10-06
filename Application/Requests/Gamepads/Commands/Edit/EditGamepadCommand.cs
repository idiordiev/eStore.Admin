using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Commands.Edit
{
    public class EditGamepadCommand : IRequest<GamepadResponse>
    {
        public int GamepadId { get; set; }
        public GamepadRequest Gamepad { get; set; }
    }
}