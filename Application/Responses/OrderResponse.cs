using System;

namespace eStore_Admin.Application.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime TimeStamp { get; set; }
    public int CustomerId { get; set; }
    public int Status { get; set; }
    public decimal Total { get; set; }
    public string ShippingCountry { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingPostalCode { get; set; }
}