using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace LubyTasks.Domain.Utils
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

        public void GetTokenData(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var currentUser = jwtHandler.ReadJwtToken(token.Replace("Bearer ", ""));
            var claims = currentUser.Claims.ToArray();

            Id = Convert.ToInt32(claims[0].Value);
            Login = claims[1].Value;
            Token = token;
        }
    }
}
