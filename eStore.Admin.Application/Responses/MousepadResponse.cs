using System;

namespace eStore.Admin.Application.Responses;

public class MousepadResponse
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
    public bool IsStitched { get; set; }
    public string TopMaterial { get; set; }
    public string BottomMaterial { get; set; }
    public string Backlight { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}