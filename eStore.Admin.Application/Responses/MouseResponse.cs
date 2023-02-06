using System;

namespace eStore.Admin.Application.Responses;

public class MouseResponse
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ManufacturerId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string BigImageUrl { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public int ButtonsQuantity { get; set; }
    public string SensorName { get; set; }
    public int MinSensorDPI { get; set; }
    public int MaxSensorDPI { get; set; }
    public string ConnectionType { get; set; }
    public string Backlight { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
}