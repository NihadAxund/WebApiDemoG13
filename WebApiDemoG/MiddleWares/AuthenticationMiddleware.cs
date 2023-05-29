using System.Buffers.Text;
using System.Security.Claims;
using System.Text;

namespace WebApiDemoG.MiddleWares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await _next(context); // may change
                return;
            }

            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                string credentialString = "";
                try
                {
                    credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }
                var credentials = credentialString.Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (username == "Niahd" && password == "NihaD123") // For testing purposea
                {
                    var claim = new[]
                    {
                        new Claim("username", username),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var identity = new ClaimsIdentity(claim, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                await _next(context);
            }
        }
    }
}