using eStore_Admin.Application.Interfaces.DataTransferObjects.Shared;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects
{
    public interface ICustomerDto : IEntityDto
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string Country { get; set; }
        string City { get; set; }
        string Address { get; set; }
        string PostalCode { get; set; }
        int ShoppingCartId { get; set; }
    }
}