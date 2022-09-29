using System;

namespace eStore_Admin.Application.DataTransferObjects
{
    public class GamepadDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string BigImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int ConnectionTypeId { get; set; }
        public int FeedbackId { get; set; }
        public float Weight { get; set; }
    }
}