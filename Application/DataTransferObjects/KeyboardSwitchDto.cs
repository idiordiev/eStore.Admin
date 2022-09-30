namespace eStore_Admin.Application.DataTransferObjects
{
    public class KeyboardSwitchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsTactile { get; set; }
        public bool IsClicking { get; set; }
    }
}