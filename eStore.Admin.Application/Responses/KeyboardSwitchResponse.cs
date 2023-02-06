namespace eStore.Admin.Application.Responses;

public class KeyboardSwitchResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public bool IsTactile { get; set; }
    public bool IsClicking { get; set; }
}