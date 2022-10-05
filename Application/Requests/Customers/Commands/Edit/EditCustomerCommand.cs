using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Edit
{
    public class EditCustomerCommand : IRequest<CustomerResponse>
    {
        public int CustomerId { get; set; }
        public CustomerRequest Customer { get; set; }
    }
}