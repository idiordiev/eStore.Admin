using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.SetAsDeleted
{
    public class SetCustomerAsDeletedCommand : IRequest<bool>
    {
        public SetCustomerAsDeletedCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}