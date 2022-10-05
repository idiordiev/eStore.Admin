namespace eStore_Admin.Application.RequestModels
{
    public class KeyboardSwitchRequest
    {
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsTactile { get; set; }
        public bool IsClicking { get; set; }
    }
}