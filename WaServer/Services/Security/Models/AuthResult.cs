namespace WaServer.Services.Security.Models
{
    public class AuthResult
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
