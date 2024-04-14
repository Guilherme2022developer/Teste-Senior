using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace SêniorTeste.API.Config
{
    public class SigningCredentialsConfiguration
    {
        public static readonly byte[] Key = Encoding.ASCII.GetBytes("hfdhshfhwfejkhjfdfjhsjk44f4f5ds4fed4");
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            var signingKey = new SymmetricSecurityKey(Key);
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
