using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnionArch.Data.Entities.Identity;
using OnionArch.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Repository.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        private readonly SymmetricSecurityKey key;

        public TokenService(IConfiguration config)
        {
            this.config = config;

            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            };

           var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDecrip = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = creds,
                Issuer = config["Token:Issuer"]

            };


            var tokenHandleer = new JwtSecurityTokenHandler();
            var token = tokenHandleer.CreateToken(tokenDecrip);



            return tokenHandleer.WriteToken(token);
         }
    }
}
