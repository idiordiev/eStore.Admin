using System.Collections.Generic;

namespace eStore_Admin.Application.FilterModels
{
    public class CustomerFilterModel
    {
        public ICollection<bool?> IsDeletedValues { get; set; }
        public string FirstNameSearchQuery { get; set; }
        public string LastNameSearchQuery { get; set; }
        public string EmailSearchQuery { get; set; }
        public string PhoneNumberSearchQuery { get; set; }
        public string CountrySearchQuery { get; set; }
        public string CitySearchQuery { get; set; }
        public string AddressSearchQuery { get; set; }
        public string PostalCodeSearchQuery { get; set; }
    }
}