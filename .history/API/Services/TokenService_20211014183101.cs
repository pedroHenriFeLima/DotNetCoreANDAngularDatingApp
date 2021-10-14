using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
     //login and register method passed json web token instead of appUser entity with username and password
    public class TokenService : ITokenService
    {
        //tthis key will be used as a key and it will be used to encrypt and decrypt
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            //whenever TokenService is called a key is generated
            //SymmetricSecurityKey receives a byte
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            //token is composed of
            //credentials/ claims / other information
            var claims = new List<Claim> {
                //using System.IdentityModel.Tokens.Jwt;
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };
            //create credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //describe token - how it will look
            var tokenDescription = new SecurityTokenDescriptor
            { 
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler =  new JwtSecurityTokenHandler();
            var token  = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}