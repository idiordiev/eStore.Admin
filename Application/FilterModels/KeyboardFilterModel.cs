using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.FilterModels
{
    public class KeyboardFilterModel
    {
        public ICollection<bool?> IsDeletedValues { get; set; }
        public string NameSearchString { get; set; }
        public ICollection<int> ManufacturerIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime CreatedStartDate { get; set; }
        public DateTime CreatedEndDate { get; set; }
        public ICollection<int> KeyboardTypeIds { get; set; }
        public ICollection<int> KeyboardSizeIds { get; set; }
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int?> SwitchIds { get; set; }
        public ICollection<int> KeyRolloverIds { get; set; }
        public ICollection<int> BacklightIds { get; set; }
    }
}