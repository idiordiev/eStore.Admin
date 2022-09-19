using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.Interfaces.FilterModels
{
    public interface IMousepadFilterModel
    {
        ICollection<bool?> IsDeletedValues { get; set; }
        string NameSearchString { get; set; }
        ICollection<int> ManufacturerIds { get; set; }
        decimal? MinPrice { get; set; }
        decimal? MaxPrice { get; set; }
        DateTime CreatedStartDate { get; set; }
        DateTime CreatedEndDate { get; set; }
        ICollection<bool> IsStitchedValues { get; set; }
        ICollection<int> BottomMaterialIds { get; set; }
        ICollection<int> TopMaterialIds { get; set; }
        ICollection<int> BacklightIds { get; set; }
    }
}