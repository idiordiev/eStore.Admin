using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.KeyboardSwitches.Queries.GetById
{
    public class GetKeyboardSwitchByIdQuery : IRequest<KeyboardSwitchResponse>
    {
        public GetKeyboardSwitchByIdQuery(int switchId)
        {
            SwitchId = switchId;
        }

        public int SwitchId { get; }
    }
}