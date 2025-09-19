using Microsoft.AspNetCore.Http;
using System.Net;

namespace UdemyMicroservice.Shared.Extensions
{
    public static class EndpointResultExtension
    {
        public static IResult ToGenericResult<T>(this ServiceResult<T> serviceResult)
        {
            return serviceResult.StatusCode switch
            {
                HttpStatusCode.OK => Results.Ok(serviceResult.Data),
                HttpStatusCode.Created => Results.Created(serviceResult.UrlAsCreated, serviceResult.Data),
                HttpStatusCode.NotFound => Results.NotFound(serviceResult.Fail!),
                _ => Results.Problem(serviceResult.Fail!)
            };
        }

        public static IResult ToResult(this ServiceResult serviceResult)
        {
            return serviceResult.StatusCode switch
            {
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(serviceResult.Fail!),
                _ => Results.Problem(serviceResult.Fail!)
            };
        }
    }
}