using GateWayApi.Services.Models;
using GateWayApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace GateWayApi.DAL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        
    }
}
