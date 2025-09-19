using System.ComponentModel.DataAnnotations;

namespace UdemyMicroservice.Catalog.Api.Options
{
    public class MongoOptions
    {
        [Required]
        public string DatabaseName { get; set; } = default!;

        [Required]
        public string ConnectionString { get; set; } = default!;
    }
}