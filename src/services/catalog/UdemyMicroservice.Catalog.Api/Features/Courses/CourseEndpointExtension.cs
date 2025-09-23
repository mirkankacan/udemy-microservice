using Asp.Versioning.Builder;
using UdemyMicroservice.Catalog.Api.Features.Courses.Create;
using UdemyMicroservice.Catalog.Api.Features.Courses.Delete;
using UdemyMicroservice.Catalog.Api.Features.Courses.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Courses.GetAllByUserId;
using UdemyMicroservice.Catalog.Api.Features.Courses.GetById;
using UdemyMicroservice.Catalog.Api.Features.Courses.Update;

namespace UdemyMicroservice.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Courses")
                .WithDescription("Course management endpoints for creating, reading, updating, and deleting courses")
                .CreateCourseGroupItemEndpoint()
                .GetAllCourseGroupItemEndpoint()
                .GetCourseByIdGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .GetAllCourseByUserIdGroupItemEndpoint()
                .RequireAuthorization();
        }
    }
}