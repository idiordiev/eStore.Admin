namespace eStore_Admin.Application.RequestModels
{
    public class OrderItemRequest
    {
        public bool IsDeleted { get; set; }
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int GoodsId { get; set; }
        public int Quantity { get; set; }
    }
}