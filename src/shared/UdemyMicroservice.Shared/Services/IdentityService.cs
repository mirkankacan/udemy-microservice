namespace UdemyMicroservice.Shared.Services
{
    public class IdentityService : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("4a600000-5867-7c57-d0a7-08ddf77eae11");

        public string GetUserName => "Test User";
    }
}