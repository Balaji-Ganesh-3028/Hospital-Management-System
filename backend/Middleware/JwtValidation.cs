using Microsoft.AspNetCore.Authorization;

namespace backend.Middleware
{
    public class JwtValidation
    {
        private readonly RequestDelegate _requestDelegate;

        public JwtValidation(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

            if (allowAnonymous)
            {
                // Skip JWT validation for [AllowAnonymous] endpoints
                await _requestDelegate(context);
                return;
            }
            // Logic to validate JWT token
            // You can access the token from the Authorization header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: No token provided");
                return;
            }

            // If the token is valid, proceed to the next middleware/component
            await _requestDelegate(context);
        }
    }
}
