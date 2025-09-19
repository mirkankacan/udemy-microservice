namespace UdemyMicroservice.Discount.Api.Data
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        protected BaseEntity()
        {
            Id = NewId.NextSequentialGuid();
        }
    }
}