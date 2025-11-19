using backend.Enum;
using backend.utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace backend.CustomAttributes
{
    public class CustomAuth: Attribute, IAuthorizationFilter
    {
        private readonly List<Roles> allowedRoles = null;

        public CustomAuth(params Roles[] roles)
        {
            allowedRoles = new List<Roles>(roles);
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
                // Convert string claim to enum
                if (!System.Enum.TryParse(userRoleClaim, true, out Roles userRoleEnum))
                {
                    context.Result = new UnauthorizedObjectResult("Invalid role in token");
                    return;
                }

                if (!allowedRoles.Contains(userRoleEnum))
                {
                    context.Result = new UnauthorizedObjectResult("User is not authorized to access the API");
                    return;
                }
            }
        }
    }
}
