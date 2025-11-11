using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.utilities
{
    public class TokenDecode
    {
        public static IEnumerable<Claim> DecodeJWTToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var claims = jwt.Claims.Select(claim => claim);
            Console.WriteLine(claims);
            return claims;
        }
    }
}
