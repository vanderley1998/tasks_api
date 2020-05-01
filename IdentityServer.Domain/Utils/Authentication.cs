﻿using IdentyServer.Domain.Queries.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IdentityServer.Domain.Utils
{
    public static class Authentication
    {
        public static object GetToken(this CredentialUser user)
        {
            if (user == null)
                return null;

            var rights = new[]
            {
                new Claim(nameof(user.Id), Convert.ToString(user.Id)),
                new Claim(nameof(user.Login), user.Login)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("WmZq4t7w!z$C&F)J@NcRfUjXn2r5u8x/"));
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