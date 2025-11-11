using backend.models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.utilities
{
    public static class GenerateToken
    {
        private static readonly string securitykey;

        static GenerateToken()
        {
            securitykey = "aP9sD8fG7hJ2kL5mN8pQ2rT5vX8yZ3bC";
        }


        public static string CreateToken(ClaimsItems claims)
        {
            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim("role", claims.roleName));
            userClaims.Add(new Claim("email", claims.email));
            userClaims.Add(new Claim("userName", claims.userName));

            if (!string.IsNullOrEmpty(claims.roleId))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddMinutes(120),
                    claims: userClaims,
                    signingCredentials: creds
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenString;
            }
            return string.Empty;
        }
    }
}
