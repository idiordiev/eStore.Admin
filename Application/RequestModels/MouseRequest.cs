namespace eStore_Admin.Application.RequestModels
{
    public class MouseRequest
    {
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string BigImageUrl { get; set; }
        public int ButtonsQuantity { get; set; }
        public string SensorName { get; set; }
        public int MinSensorDPI { get; set; }
        public int MaxSensorDPI { get; set; }
        public int ConnectionTypeId { get; set; }
        public int BacklightId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}