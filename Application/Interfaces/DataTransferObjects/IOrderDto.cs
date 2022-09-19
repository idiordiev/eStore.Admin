using System;
using eStore_Admin.Application.Interfaces.DataTransferObjects.Shared;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects
{
    public interface IOrderDto : IEntityDto
    {
        DateTime TimeStamp { get; set; }
        int CustomerId { get; set; }
        int Status { get; set; }
        decimal Total { get; set; }
        string ShippingCountry { get; set; }
        string ShippingCity { get; set; }
        string ShippingAddress { get; set; }
        string ShippingPostalCode { get; set; }
    }
}