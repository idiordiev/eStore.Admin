namespace eStore_Admin.Application.RequestDTOs;

public class OrderItemDto
{
    public bool IsDeleted { get; set; }
    public int GoodsId { get; set; }
    public int Quantity { get; set; }
}