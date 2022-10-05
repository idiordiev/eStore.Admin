using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponse>
    {
        public int CustomerId { get; }
        
        public GetCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}