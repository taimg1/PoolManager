using PoolMS.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace PoolMS.UI.Middleware
{
    public class AuthJWTMiddleware
    {
        private readonly RequestDelegate _next;
      
        public AuthJWTMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                if (context.Request.Method.Equals("POST") || context.Request.Method.Equals("PUT"))
                {
                    string body;
                    context.Request.EnableBuffering();
                    using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                    {
                        body = await reader.ReadToEndAsync();
                        context.Request.Body.Position = 0;
                    }

                    var token = ExtractTokenFromBody(body);
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jsonToken != null)
                    {
                        var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                        context.Items["email"] = email;
                    }
                }
            }

            await _next(context);
        }

        private string ExtractTokenFromBody(string body)
        {
            var jsonDocument = JsonDocument.Parse(body);
            if (jsonDocument.RootElement.TryGetProperty("token", out var tokenProperty))
            {
                return tokenProperty.GetString();
            }

            return null;
        }
    }
    public static class TokenFromBodyMiddlewareExtension
    {
        public static IApplicationBuilder UseTokenFromBodyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthJWTMiddleware>();
        }
    }
}
