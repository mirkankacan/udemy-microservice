using MongoDB.Bson.Serialization.Attributes;

namespace UdemyMicroservice.Catalog.Api.Repositories
{
    public abstract class BaseEntity
    {
        // Snow flake algoritması
        [BsonElement("_id")]
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