namespace UdemyMicroservice.Order.Domain.Entities
{
    public abstract class BaseEntity<TEntityId>
    {
        public TEntityId Id { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}