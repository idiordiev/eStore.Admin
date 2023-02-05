using System;

namespace eStore_Admin.Application.Responses;

public class KeyboardResponse
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string BigImageUrl { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public int? SwitchId { get; set; }
    public string KeycapMaterial { get; set; }
    public string FrameMaterial { get; set; }
    public string KeyRollover { get; set; }
    public string Backlight { get; set; }
    public string ConnectionType { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
}