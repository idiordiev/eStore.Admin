using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Add
{
    public class AddKeyboardSwitchCommand : IRequest<KeyboardSwitchResponse>
    {
        public KeyboardSwitchRequest KeyboardSwitch { get; set; }
    }
}