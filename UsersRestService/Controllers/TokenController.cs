using Microsoft.AspNetCore.Mvc;
using UsersRestService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersRestService.Controllers
{
    public class TokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // This should require SSL
        [HttpPost]
        public dynamic Post([FromBody] TokenRequest tokenRequest)
        {
            var token = TokenHelper.GetToken(tokenRequest.Email, tokenRequest.Password);
            return new { Token = token };
        }

        // This should require SSL
        [HttpGet("{userEmail}/{password}")]
        public dynamic Get(string userEmail, string password)
        {
            var token = TokenHelper.GetToken(userEmail, password);
            return new { Token = token };
        }
    }
}
