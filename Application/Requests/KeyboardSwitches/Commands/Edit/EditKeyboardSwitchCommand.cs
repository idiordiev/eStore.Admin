using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Edit
{
    public class EditKeyboardSwitchCommand : IRequest<KeyboardSwitchResponse>
    {
        public EditKeyboardSwitchCommand(int switchId)
        {
            SwitchId = switchId;
        }

        public int SwitchId { get; }
        public KeyboardSwitchDto KeyboardSwitch { get; set; }
    }
}