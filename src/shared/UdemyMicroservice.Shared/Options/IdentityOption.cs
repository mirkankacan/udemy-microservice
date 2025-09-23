using System.ComponentModel.DataAnnotations;

namespace UdemyMicroservice.Shared.Options
{
    public class IdentityOption
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        public string Issuer { get; set; }
    }
}