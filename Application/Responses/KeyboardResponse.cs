using System;

namespace eStore_Admin.Application.Responses
{
    public class KeyboardResponse
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
        public int TypeId { get; set; }
        public int SizeId { get; set; }
        public int? SwitchId { get; set; }
        public int KeycapMaterialId { get; set; }
        public int FrameMaterialId { get; set; }
        public int KeyRolloverId { get; set; }
        public int BacklightId { get; set; }
        public int ConnectionTypeId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}