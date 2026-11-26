using AuthApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Services
{
    public class GenerateToken
    {
        public string Token(Guid id, string email, ICollection<Role> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ide-johet-az-egyedi-azonosito-amivel-a-token-felhasznalas-szabalyozhato-legalabb-32-karakter-legyen");

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            foreach(var r in roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, r.Name!));
            }

            var tokenDescription = new SecurityTokenDescriptor
            {
                Audience = "client-api",
                Issuer = "auth-api",
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
           
        }
    }
}
