using System.Collections.Generic;

namespace eStore_Admin.Application.RequestDTOs;

public class OrderDto
{
    public bool IsDeleted { get; set; }
    public int CustomerId { get; set; }
    public int Status { get; set; }
    public string ShippingCountry { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingPostalCode { get; set; }
    public ICollection<OrderItemDto> ItemsToAdd { get; set; }
}