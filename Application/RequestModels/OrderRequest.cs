namespace eStore_Admin.Application.RequestModels
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public int Status { get; set; }
        public decimal Total { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }
    }
}