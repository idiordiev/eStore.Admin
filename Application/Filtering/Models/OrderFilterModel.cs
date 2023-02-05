using System;
using System.Collections.Generic;

namespace eStore_Admin.Application.Filtering.Models;

public class OrderFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public ICollection<int> CustomerIds { get; set; }
    public ICollection<int> StatusValues { get; set; }
    public decimal? MinTotal { get; set; }
    public decimal? MaxTotal { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
}