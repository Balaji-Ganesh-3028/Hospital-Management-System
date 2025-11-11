using backend.utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.CustomAttributes
{
    public class CustomAuth: Attribute, IAuthorizationFilter
    {
        private readonly List<string> allowedRoles = null;

        public CustomAuth(params string[] roles)
        {
            allowedRoles = new List<string>(roles);
            Console.WriteLine("Allowed Roles: " + string.Join(", ", allowedRoles));
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Custom authorization logic can be implemented here
            // For example, checking for a specific header or claim
            var hasAuthHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            var userRole = hasAuthHeader.Split(' ');

            if (userRole.Length > 0)
            {
                var decodedToken = new TokenDecode();
                var claims = TokenDecode.DecodeJWTToken(userRole[1]);

                // Extract specific claims
                var userRoleClaim = claims.FirstOrDefault(c => c.Type == "role")?.Value;
                var emailClaim = claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var userNameClaim = claims.FirstOrDefault(c => c.Type == "userName")?.Value;

                Console.WriteLine("User Role from Token: " + userRoleClaim);
                Console.WriteLine("allowedRoles: " + allowedRoles);
                if (!allowedRoles.Contains(userRoleClaim))
                {
                    context.Result = new UnauthorizedObjectResult("User is not authorized to access the API");
                }
            }
        }
    }
}
