using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Commands.Add
{
    public class AddKeyboardCommand : IRequest<KeyboardResponse>
    {
        public KeyboardRequest Keyboard { get; set; }
    }
}