using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Mouses.Queries.GetById
{
    public class GetMouseByIdQuery : IRequest<MouseResponse>
    {
        public GetMouseByIdQuery(int mouseId)
        {
            MouseId = mouseId;
        }

        public int MouseId { get; }
    }
}