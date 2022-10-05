using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Delete
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int CustomerId { get; }
        
        public DeleteCustomerCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}