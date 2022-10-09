using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.Filtering.Models
{
    public class MouseFilterModel
    {
        public ICollection<bool> IsDeletedValues { get; set; }
        public string Name { get; set; }
        public ICollection<string> Manufacturers { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime CreatedStartDate { get; set; }
        public DateTime CreatedEndDate { get; set; }
        public float? MinWeight { get; set; }
        public float? MaxWeight { get; set; }
        public ICollection<string> ConnectionTypes { get; set; }
        public ICollection<string> Backlights { get; set; }
    }
}