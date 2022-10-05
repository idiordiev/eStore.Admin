using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.Delete
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public DeleteCustomerCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}