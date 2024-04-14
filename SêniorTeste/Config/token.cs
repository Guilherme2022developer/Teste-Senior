using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SêniorTeste.API.Config
{
    public class Token
    {
        public string GenerateJwtToken(string userId, string userName, DateTime expiryDate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = SigningCredentialsConfiguration.Key;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            }),
                Expires = expiryDate,
                Audience = "https://localhost",
                Issuer = "TesteSenior",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
