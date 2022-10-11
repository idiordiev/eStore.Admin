using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Edit
{
    public class EditCustomerCommand : IRequest<CustomerResponse>
    {
        public EditCustomerCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
        public CustomerDto Customer { get; set; }
    }
}