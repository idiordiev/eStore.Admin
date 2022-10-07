using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetAllPaged
{
    public class GetAllCustomersPagedQuery : IRequest<IEnumerable<CustomerResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
    }
}