using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Commands.Delete
{
    public class DeleteMousepadCommand : IRequest<bool>
    {
        public DeleteMousepadCommand(int mousepadId)
        {
            MousepadId = mousepadId;
        }

        public int MousepadId { get; }
    }
}