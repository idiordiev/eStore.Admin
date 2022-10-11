using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Add
{
    public class AddCustomerCommand : IRequest<CustomerResponse>
    {
        public CustomerDto Customer { get; set; }
    }
}