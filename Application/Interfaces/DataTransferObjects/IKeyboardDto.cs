using eStore_Admin.Application.Interfaces.DataTransferObjects.Shared;

namespace eStore_Admin.Application.Interfaces.DataTransferObjects
{
    public interface IKeyboardDto : IGoodsDto
    {
        int TypeId { get; set; }
        int SizeId { get; set; }
        int? SwitchId { get; set; }
        int KeycapMaterialId { get; set; }
        int FrameMaterialId { get; set; }
        int KeyRolloverId { get; set; }
        int BacklightId { get; set; }
        int ConnectionTypeId { get; set; }
        float Length { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        float Weight { get; set; }
    }
}