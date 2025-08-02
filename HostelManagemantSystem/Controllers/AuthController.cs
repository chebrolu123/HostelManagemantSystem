using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.HostelServices.Services;
using HostelManagemantSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagemantSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _iuserservice;

        public AuthController(IUserServices iuserservice)
        {
            _iuserservice = iuserservice;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAuth login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
             
            var user = await _iuserservice.LoginUserAsync(login.Email, login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(new
            {
                message = "Login Successful",
                user = new
                {
                    id = user.ID,
                    firstName = user.FirstName,
                    role = user.Role.Name
                }
            });
        }

    }
}
