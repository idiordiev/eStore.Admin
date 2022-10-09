using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Commands.Delete
{
    public class DeleteMouseCommand : IRequest<bool>
    {
        public DeleteMouseCommand(int mouseId)
        {
            MouseId = mouseId;
        }

        public int MouseId { get; }
    }
}