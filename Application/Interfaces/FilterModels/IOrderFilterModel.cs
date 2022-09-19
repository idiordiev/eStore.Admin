using System.Collections.Generic;

namespace eStore_Admin.Application.Interfaces.FilterModels
{
    public interface IOrderFilterModel
    {
        ICollection<bool?> IsDeletedValues { get; set; }
        ICollection<int> CustomerIds { get; set; }
        ICollection<int> StatusValues { get; set; }
        decimal? MinTotal { get; set; }
        decimal? MaxTotal { get; set; }
        string CountrySearchQuery { get; set; }
        string CitySearchQuery { get; set; }
        string AddressSearchQuery { get; set; }
        string PostalCodeSearchQuery { get; set; }
    }
}