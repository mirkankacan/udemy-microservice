using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace UdemyMicroservice.Shared.Services
{
    public class IdentityService(IHttpContextAccessor accessor) : IIdentityService
    {
        public Guid GetUserId
        {
            get
            {
                if (!accessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                return Guid.Parse(accessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value!);
            }
        }

        public string GetUserName
        {
            get
            {
                if (!accessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                return accessor.HttpContext!.User.Identity!.Name;
            }
        }

        public List<string> GetUserRoles
        {
            get
            {
                if (!accessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                return accessor.HttpContext!.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            }
        }
    }
}