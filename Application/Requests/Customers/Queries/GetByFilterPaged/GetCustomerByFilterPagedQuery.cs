using System.Collections.Generic;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetByFilterPaged
{
    public class GetCustomerByFilterPagedQuery : IRequest<IEnumerable<CustomerResponse>>
    {
        public PagingParameters PagingParameters { get; set; }
        public CustomerFilterModel FilterModel { get; set; }
    }
}