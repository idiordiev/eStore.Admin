using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Add
{
    public class AddCustomerCommand : IRequest<CustomerResponse>
    {
        public CustomerRequest Customer { get; set; }
    }
}