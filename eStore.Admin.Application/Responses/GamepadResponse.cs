using System;

namespace eStore.Admin.Application.Responses;

public class GamepadResponse
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
    public string ConnectionType { get; set; }
    public string Feedback { get; set; }
    public float Weight { get; set; }
}