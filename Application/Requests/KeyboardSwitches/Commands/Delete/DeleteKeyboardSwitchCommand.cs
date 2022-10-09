using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Delete
{
    public class DeleteKeyboardSwitchCommand : IRequest<bool>
    {
        public DeleteKeyboardSwitchCommand(int switchId)
        {
            SwitchId = switchId;
        }

        public int SwitchId { get; }
    }
}