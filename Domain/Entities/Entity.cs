namespace eStore_Admin.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}