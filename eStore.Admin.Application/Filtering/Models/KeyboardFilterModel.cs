using System;
using System.Collections.Generic;

namespace eStore.Admin.Application.Filtering.Models;

public class KeyboardFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public string Name { get; set; }
    public ICollection<string> Manufacturers { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? CreatedStartDate { get; set; }
    public DateTime? CreatedEndDate { get; set; }
    public ICollection<string> Types { get; set; }
    public ICollection<string> Sizes { get; set; }
    public ICollection<string> ConnectionTypes { get; set; }
    public ICollection<int?> SwitchIds { get; set; }
    public ICollection<string> KeyRollovers { get; set; }
    public ICollection<string> Backlights { get; set; }
}