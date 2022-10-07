using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetById
{
    public class GetKeyboardByIdQuery : IRequest<KeyboardResponse>
    {
        public GetKeyboardByIdQuery(int keyboardId)
        {
            KeyboardId = keyboardId;
        }

        public int KeyboardId { get; }
    }
}