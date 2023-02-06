namespace eStore.Admin.Application.RequestDTOs;

public class KeyboardSwitchDto
{
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public bool IsTactile { get; set; }
    public bool IsClicking { get; set; }
}