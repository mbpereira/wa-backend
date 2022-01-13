using WaServer.Data.Entities;

namespace WaServer.Services.Security.Contracts
{
    public interface ITokenGenerator
    {
        string Generate(User user);
    }
}
