using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetAllPaged
{
    public class GetAllCustomersPagedQuery : IRequest<IEnumerable<CustomerResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}