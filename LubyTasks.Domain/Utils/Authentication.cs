using LubyTasks.Domain.Queries.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LubyTasks.Domain.Utils
{
    public static class Authentication
    {
        public static string KeyJwt { get; set; }
        public static object GetToken(this CredentialUser user)
        {
            if (user == null)
                return null;

            var rights = new[]
            {
                new Claim(nameof(user.Id).ToLower(), Convert.ToString(user.Id)),
                new Claim(nameof(user.Login).ToLower(), user.Login)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(KeyJwt));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: rights,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(30)
            );

            return new { token = new JwtSecurityTokenHandler().WriteToken(token) };
        }
    }
}
