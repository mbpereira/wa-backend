using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WaServer.Data.Entities;
using WaServer.Services.Security.Contracts;

namespace WaServer.Services.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenValidationParameters _parameters;

        public TokenGenerator(TokenValidationParameters parameters)
        {
            _parameters = parameters;
        }

        public string Generate(User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(_parameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtHandler.CreateToken(tokenDescriptor);

            return jwtHandler.WriteToken(token);
        }
    }
}
