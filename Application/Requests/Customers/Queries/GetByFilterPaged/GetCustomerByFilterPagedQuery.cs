using System.Collections.Generic;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetByFilterPaged
{
    public class GetCustomerByFilterPagedQuery : IRequest<IEnumerable<CustomerResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
        public ICollection<bool?> IsDeletedValues { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}