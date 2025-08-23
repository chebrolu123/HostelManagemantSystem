using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.HostelServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace HostelManagemantSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDasboardServices _adminDasboardServices;

        public AdminDashboardController(IAdminDasboardServices adminDasboardServices)
        {
            _adminDasboardServices = adminDasboardServices;
        }

        [HttpGet("dashboard")]
        //[Authorize(Roles = "HostelAdmin")]
        public async Task<IActionResult> GetDashboard()
        {
            int hostelId = 1;
           
            var dashboard = await _adminDasboardServices.GetAdminDashboardAsync(hostelId);
            return Ok(dashboard);
        }
    }
}
