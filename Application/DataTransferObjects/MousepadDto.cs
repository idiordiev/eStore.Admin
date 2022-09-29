using System;

namespace eStore_Admin.Application.DataTransferObjects
{
    public class MousepadDto
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
        public bool IsStitched { get; set; }
        public int TopMaterialId { get; set; }
        public int BottomMaterialId { get; set; }
        public int BacklightId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}