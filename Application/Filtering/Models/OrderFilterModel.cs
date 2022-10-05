using System.Collections.Generic;

namespace eStore_Admin.Application.Filtering.Models
{
    public class OrderFilterModel
    {
        public ICollection<bool?> IsDeletedValues { get; set; }
        public ICollection<int> CustomerIds { get; set; }
        public ICollection<int> StatusValues { get; set; }
        public decimal? MinTotal { get; set; }
        public decimal? MaxTotal { get; set; }
        public string CountrySearchQuery { get; set; }
        public string CitySearchQuery { get; set; }
        public string AddressSearchQuery { get; set; }
        public string PostalCodeSearchQuery { get; set; }
    }
}