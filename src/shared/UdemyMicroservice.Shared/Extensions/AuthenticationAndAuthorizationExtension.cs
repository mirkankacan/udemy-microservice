using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using UdemyMicroservice.Shared.Options;

namespace UdemyMicroservice.Shared.Extensions
{
    public static class AuthenticationAndAuthorizationExtension
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var identityOptions = configuration.GetSection("IdentityOptions").Get<IdentityOption>()!;
            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = identityOptions.Address;
                    options.Audience = identityOptions.Audience;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        RoleClaimType = "roles",
                        NameClaimType = "preferred_username"
                    };
                })
                .AddJwtBearer("ClientCredentialsScheme", options =>
                {
                    options.Authority = identityOptions.Address;
                    options.Audience = identityOptions.Audience;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateIssuer = true
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientCredentials", policy =>
                {
                    policy.AuthenticationSchemes.Add("ClientCredentialsScheme");
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("client_id");
                });
                options.AddPolicy("Password", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);
                });
            });
            return services;
        }
    }
}