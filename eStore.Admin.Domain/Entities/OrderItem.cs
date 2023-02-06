namespace eStore.Admin.Domain.Entities;

public class OrderItem : Entity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int GoodsId { get; set; }
    public Goods Goods { get; set; }
}