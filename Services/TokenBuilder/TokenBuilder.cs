using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace disclone_api.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        public string BuildToken(int id)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.Settings["EncryptionKey"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}