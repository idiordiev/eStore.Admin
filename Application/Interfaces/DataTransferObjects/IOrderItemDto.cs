using eStore_Admin.Application.Interfaces.DataTransferObjects.Shared;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects
{
    public interface IOrderItemDto : IEntityDto
    {
        int OrderId { get; set; }
        decimal UnitPrice { get; set; }
        int GoodsId { get; set; }
        int Quantity { get; set; }
    }
}