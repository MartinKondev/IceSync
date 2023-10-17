using System.IdentityModel.Tokens.Jwt;

namespace IceSync.Domain
{
    public class Common
    {
        public static DateTimeOffset GetBearerExpiration(string bearer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(bearer);
            var expirationDateUnix = int.Parse(jwtSecurityToken.Claims.First(x => x.Type == "exp").Value);
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(expirationDateUnix);

            return dateTime;
        }
    }
}
