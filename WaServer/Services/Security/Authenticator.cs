
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WaServer.Data;
using WaServer.Services.Security.Contracts;
using WaServer.Services.Security.Models;

namespace WaServer.Services.Auth
{
    public class Authenticator : IAuthenticationService
    {
        private readonly SimpleEcommerceContext _context;
        private readonly ITokenGenerator _tokenGenerator;

        public Authenticator(SimpleEcommerceContext context, ITokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResult> Attempt(Credentials credentials)
        {
            const string invalidCredentialsMessage = "Usuário ou senha inválidos.";

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == credentials.Email);

            if (user == null)
            {
                throw new System.Exception(invalidCredentialsMessage);
            }

            if(!BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password))
            {
                throw new System.Exception(invalidCredentialsMessage);
            }

            return new AuthResult()
            {
                IdUser = user.IdUser,
                Email = user.Email,
                Name = user.Name,
                Token = _tokenGenerator.Generate(user)
            };
        }
    }
}
