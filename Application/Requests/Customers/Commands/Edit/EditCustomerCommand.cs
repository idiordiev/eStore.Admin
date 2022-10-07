using eStore_Admin.Application.RequestModels;
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
        public CustomerRequest Customer { get; set; }
    }
}