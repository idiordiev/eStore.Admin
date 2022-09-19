using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.Interfaces.FilterModels
{
    public interface IGamepadFilterModel
    {
        ICollection<bool?> IsDeletedValues { get; set; }
        string NameSearchString { get; set; }
        ICollection<int> ManufacturerIds { get; set; }
        decimal? MinPrice { get; set; }
        decimal? MaxPrice { get; set; }
        DateTime CreatedStartDate { get; set; }
        DateTime CreatedEndDate { get; set; }
        ICollection<int> ConnectionTypeIds { get; set; }
        ICollection<int> FeedbackIds { get; set; }
        float? MinWeight { get; set; }
        float? MaxWeight { get; set; }
    }
}