using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.FilterModels
{
    public class GamepadFilterModel
    {
        public ICollection<bool?> IsDeletedValues { get; set; }
        public string NameSearchString { get; set; }
        public ICollection<int> ManufacturerIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime CreatedStartDate { get; set; }
        public DateTime CreatedEndDate { get; set; }
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int> FeedbackIds { get; set; }
        public float? MinWeight { get; set; }
        public float? MaxWeight { get; set; }
    }
}