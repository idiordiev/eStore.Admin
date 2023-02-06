namespace eStore.Admin.Application.Responses;

public class OrderItemResponse
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public int OrderId { get; set; }
    public decimal UnitPrice { get; set; }
    public int GoodsId { get; set; }
    public int Quantity { get; set; }
}