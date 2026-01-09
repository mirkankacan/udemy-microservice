using System.ComponentModel.DataAnnotations;

namespace UdemyMicroservice.Bus.Options
{
    public class BusOptions
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int Port { get; set; }
    }
}