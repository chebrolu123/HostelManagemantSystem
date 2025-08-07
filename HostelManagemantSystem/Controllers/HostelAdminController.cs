using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagemantSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelAdminController : ControllerBase
    {
        private readonly IAdminservices _adminService;
        public HostelAdminController(IAdminservices adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("GetHostelNames")]
        public async Task<IActionResult> GetHostelNames()
        {
            var hostels = await _adminService.GetHostelNamesAsync();
            return Ok(hostels);
        }
        [HttpGet("GetAdminsByHostelId/{id}")]
        public async Task<IActionResult> GetAdminsByHostelId(int id)
        {
            var admins = await _adminService.GetAdminsByHostelIdAsync(id);
            if (admins == null || !admins.Any())
            {
                return NotFound("No admins found for the specified hostel.");
            }
            return Ok(admins);
        }
        [HttpPost("AddAdmin/{HostelId}")]
        public async Task<IActionResult> AddAdmin(int HostelId, [FromBody] AddAdmin addAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newAdmin = await _adminService.AddAdminAsync(HostelId, addAdmin);
            if (newAdmin == null)
            {
                return BadRequest("Email already exists or hostel not found.");
            }
            return CreatedAtAction(nameof(GetAdminsByHostelId), new { id = HostelId }, newAdmin);
        }
        [HttpPut("UpdateAdmin/{userId}")]
        public async Task<IActionResult> UpdateAdmin(int userId, [FromBody] UpadteAdmin updateAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedAdmin = await _adminService.UpdateAdminAsync(userId, updateAdmin);
            if (updatedAdmin == null)
            {
                return NotFound("Admin not found.");
            }
            return Ok(updatedAdmin);
        }
        [HttpPut("UpdateStatus/{userId}")]
        public async Task<IActionResult> UpdateStatus(int userId, [FromBody] UpdateStatus updateStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _adminService.UpdateStautsAsync(userId, updateStatus.IsActive);
            if (!isUpdated)
            {
                return NotFound("Admin not found or status update failed.");
            }
            return NoContent();
        }
    }
}
