namespace UdemyMicroservice.Order.Domain.Entities
{
    public class Address : BaseEntity<int>
    {
        public string Province { get; set; } = default!;
        public string District { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        public string Line { get; set; } = default!;

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}