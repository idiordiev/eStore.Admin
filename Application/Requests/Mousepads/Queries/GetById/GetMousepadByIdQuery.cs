using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mousepads.Queries.GetById
{
    public class GetMousepadByIdQuery : IRequest<MousepadResponse>
    {
        public GetMousepadByIdQuery(int mousepadId)
        {
            MousepadId = mousepadId;
        }

        public int MousepadId { get; }
    }
}