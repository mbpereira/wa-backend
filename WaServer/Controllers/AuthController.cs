using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WaServer.Services.Security.Contracts;
using WaServer.Services.Security.Models;

namespace WaServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authentication;

        public AuthController(IAuthenticationService authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Credentials credentials)
        {
            try
            {
                return ToJson(await _authentication.Attempt(credentials)); 
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
