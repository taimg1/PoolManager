using PoolMS.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace PoolMS.API.Auth
{
    public class GetUserMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                    context.Items["email"] = email;
                }
            }
            await _next(context);
        }
    }
    public static class GetUserMiddlewareExtention
    {
        public static IApplicationBuilder UseGetUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GetUserMiddleware>();
        }
    }
}
