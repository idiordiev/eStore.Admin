namespace eStore_Admin.Application.RequestModels
{
    public class GamepadRequest
    {
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string BigImageUrl { get; set; }
        public int ConnectionTypeId { get; set; }
        public int FeedbackId { get; set; }
        public float Weight { get; set; }
    }
}