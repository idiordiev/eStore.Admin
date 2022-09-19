using System;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects.Shared
{
    public interface IGoodsDto : IEntityDto
    {
        string Name { get; set; }
        int ManufacturerId { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        string ThumbnailImageUrl { get; set; }
        string BigImageUrl { get; set; }
        DateTime Created { get; set; }
        DateTime LastModified { get; set; }
    }
}