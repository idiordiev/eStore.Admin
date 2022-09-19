namespace eStore_Admin.Application.Interfaces.DataTransferObjects.Shared
{
    public interface IEntityDto
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }
}