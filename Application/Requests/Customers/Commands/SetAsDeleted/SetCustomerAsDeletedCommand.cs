using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.SetAsDeleted
{
    public class SetCustomerAsDeletedCommand : IRequest<bool>
    {
        public int CustomerId { get; }
        
        public SetCustomerAsDeletedCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}