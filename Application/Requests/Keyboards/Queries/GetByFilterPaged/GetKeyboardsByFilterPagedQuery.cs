using System.Collections.Generic;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetByFilterPaged
{
    public class GetKeyboardsByFilterPagedQuery : IRequest<IEnumerable<KeyboardResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
        public KeyboardFilterModel FilterModel { get; set; }
    }
}