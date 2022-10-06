using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetById
{
    public class GetGamepadByIdAsyncQuery : IRequest<GamepadResponse>
    {
        public GetGamepadByIdAsyncQuery(int gamepadId)
        {
            GamepadId = gamepadId;
        }

        public int GamepadId { get; }
    }
}