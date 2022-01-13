using System.Threading.Tasks;
using WaServer.Services.Security.Models;

namespace WaServer.Services.Security.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthResult> Attempt(Credentials credentials);
    }
}
