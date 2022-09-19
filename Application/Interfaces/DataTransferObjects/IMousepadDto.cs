using System;
using eStore_Admin.Application.Interfaces.DataTransferObjects.Shared;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects
{
    public interface IMousepadDto : IGoodsDto
    {
        bool IsStitched { get; set; }
        int TopMaterialId { get; set; }
        int BottomMaterialId { get; set; }
        int BacklightId { get; set; }
        float Length { get; set; }
        float Width { get; set; }
        float Height { get; set; }
    }
}