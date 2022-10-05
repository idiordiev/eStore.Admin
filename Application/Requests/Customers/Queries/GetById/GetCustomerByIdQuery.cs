using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponse>
    {
        public GetCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}