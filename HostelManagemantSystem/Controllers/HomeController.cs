using HostelManagemantSystem.HostelServices.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagemantSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public HomeController(IUserServices userServices)
        { 
            _userServices = userServices;
        }
        //private readonly IAdminservices _adminServices;
        //public HomeController(IAdminservices adminServices)
        //{
        //    _adminServices = adminServices;
        //}
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userServices.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        

    }
}
