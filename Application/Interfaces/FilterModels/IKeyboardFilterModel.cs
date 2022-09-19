using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.Interfaces.FilterModels
{
    public interface IKeyboardFilterModel
    {
        ICollection<bool?> IsDeletedValues { get; set; }
        string NameSearchString { get; set; }
        ICollection<int> ManufacturerIds { get; set; }
        decimal? MinPrice { get; set; }
        decimal? MaxPrice { get; set; }
        DateTime CreatedStartDate { get; set; }
        DateTime CreatedEndDate { get; set; }
        ICollection<int> KeyboardTypeIds { get; set; }
        ICollection<int> KeyboardSizeIds { get; set; }
        ICollection<int> ConnectionTypeIds { get; set; }
        ICollection<int?> SwitchIds { get; set; }
        ICollection<int> KeyRolloverIds { get; set; }
        ICollection<int> BacklightIds { get; set; }
    }
}