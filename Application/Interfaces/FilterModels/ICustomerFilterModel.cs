using System.Collections.Generic;

namespace eStore_Admin.Application.Interfaces.FilterModels
{
    public interface ICustomerFilterModel
    {
        ICollection<bool?> IsDeletedValues { get; set; }
        string FirstNameSearchQuery { get; set; }
        string LastNameSearchQuery { get; set; }
        string EmailSearchQuery { get; set; }
        string PhoneNumberSearchQuery { get; set; }
        string CountrySearchQuery { get; set; }
        string CitySearchQuery { get; set; }
        string AddressSearchQuery { get; set; }
        string PostalCodeSearchQuery { get; set; }
    }
}