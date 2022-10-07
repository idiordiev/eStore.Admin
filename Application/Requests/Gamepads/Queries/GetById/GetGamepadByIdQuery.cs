using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetById
{
    public class GetGamepadByIdQuery : IRequest<GamepadResponse>
    {
        public GetGamepadByIdQuery(int gamepadId)
        {
            GamepadId = gamepadId;
        }

        public int GamepadId { get; }
    }
}