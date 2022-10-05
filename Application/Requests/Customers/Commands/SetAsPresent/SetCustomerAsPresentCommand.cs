using MediatR;

namespace eStore_Admin.Application.Requests.Customers.Commands.SetAsPresent
{
    public class SetCustomerAsPresentCommand : IRequest<bool>
    {
        public SetCustomerAsPresentCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}