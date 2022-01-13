using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;

namespace WaServer.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected IActionResult ToJson(object data)
        {
            return Ok(data);
        }

        protected IActionResult Error(Exception ex, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(ex.Message);
            
            if(ex.InnerException != null)
            {
                strBuilder.Append(", ");
                strBuilder.Append(ex.InnerException.Message);
                strBuilder.Append(".");
            }

            return StatusCode(statusCode.GetHashCode(), new
            {
                message = strBuilder.ToString()
            });
        }
    }
}
