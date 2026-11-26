using AuthApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthApi.Services
{
    public class GenerateToken
    {
        public string Token(Guid id, string email, ICollection<Role> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ide-johet-az-egyedi-azonosito-amivel-a-token-felhasznalas-szabalyozhato");
        }
    }
}
