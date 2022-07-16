using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static JwtAuthentication.Database.Login;

namespace JwtAuthentication.Models
{
    public class JwtAuthenticationManager:IJwtAuthenticationManager
    {
        private readonly string Key;

        public JwtAuthenticationManager(string Key)
        {
            this.Key = Key;
        }
       
        public string GenerateToken(string username,string password)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature),
                Audience= "Admin",
                Issuer= "https://localhost:44375"

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
