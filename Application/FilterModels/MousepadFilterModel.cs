using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.FilterModels
{
    public class MousepadFilterModel
    {
        public ICollection<bool?> IsDeletedValues { get; set; }
        public string NameSearchString { get; set; }
        public ICollection<int> ManufacturerIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime CreatedStartDate { get; set; }
        public DateTime CreatedEndDate { get; set; }
        public ICollection<bool> IsStitchedValues { get; set; }
        public ICollection<int> BottomMaterialIds { get; set; }
        public ICollection<int> TopMaterialIds { get; set; }
        public ICollection<int> BacklightIds { get; set; }
    }
}