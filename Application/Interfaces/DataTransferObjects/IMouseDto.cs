using System;
using eStore_Admin.Application.Interfaces.DataTransferObjects.Shared;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects
{
    public interface IMouseDto : IGoodsDto
    {
        int ButtonsQuantity { get; set; }
        string SensorName { get; set; }
        int MinSensorDPI { get; set; }
        int MaxSensorDPI { get; set; }
        int ConnectionTypeId { get; set; }
        int BacklightId { get; set; }
        float Length { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        float Weight { get; set; }
    }
}