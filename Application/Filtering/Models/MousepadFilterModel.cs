using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.Filtering.Models;

public class MousepadFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public string Name { get; set; }
    public ICollection<string> Manufacturers { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime CreatedStartDate { get; set; }
    public DateTime CreatedEndDate { get; set; }
    public ICollection<bool> IsStitchedValues { get; set; }
    public ICollection<string> BottomMaterials { get; set; }
    public ICollection<string> TopMaterials { get; set; }
    public ICollection<string> Backlights { get; set; }
}