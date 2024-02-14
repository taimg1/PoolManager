using Microsoft.AspNetCore.Authorization;
using PoolMS.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace PoolMS.API.Auth
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                    var user = await userRepository.GetByEmail(email);

                    if (user != null)
                    {
                        var endpoint = context.GetEndpoint();

                        if (endpoint != null)
                        {
                            var authorizeAttribute = endpoint.Metadata.GetMetadata<RoleAuth>();
                            if (authorizeAttribute != null)
                            {
                                var userRole = user.Role;
                                var requiredRole = authorizeAttribute.Role;

                                if (userRole.Title == requiredRole)
                                {
                                    await _next.Invoke(context);
                                    return;
                                }
                                else
                                {
                                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                    await context.Response.WriteAsync("Forbidden: You don't have necessary permissions for the request");
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            await _next.Invoke(context);
        }
    }
    public static class AuthMiddlewareExtention
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
