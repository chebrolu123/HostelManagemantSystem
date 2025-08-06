using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.HostelServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagemantSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Route("tenant")]
    //[Authorize(Roles = "SuperAdmin")]
    public class TenentController : ControllerBase
    {
        private readonly ITenentService _tenentService;
        public TenentController(ITenentService tenentService)
        {
            _tenentService = tenentService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            //// TenantId could come from the JWT, a claim, or your user table.
            //int tenantId = int.Parse(User.FindFirst("TenantId").Value);
            int tenantId = 1;

            TenentDashboard result = await _tenentService.GetDashboardAsync(tenantId);
            return Ok(result);
        }
        [HttpPost("create-hostel")]
        public async Task<IActionResult> CreateHostel([FromBody] CreateHostel create)
        {
            var superAdminIdClaim = User.FindFirst("SuperAdminId");
            if (superAdminIdClaim == null || !int.TryParse(superAdminIdClaim.Value, out int superAdminId))
            {
                return Unauthorized("SuperAdminId claim is missing or invalid.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdHostel = await _tenentService.CreateHostelAsync(create, superAdminId);
            return CreatedAtAction(nameof(GetDashboard), new { id = createdHostel.Id }, createdHostel);
        }
        [HttpPut("hostel/{id}")]
        public async Task<IActionResult> UpdateHostel(int Id, [FromBody] UpdateHostel update)
        {
            var superAdminIdClaim = User.FindFirst("SuperAdminId");
            if (superAdminIdClaim == null || !int.TryParse(superAdminIdClaim.Value, out int superAdminId))
            {
                return Unauthorized("SuperAdminId claim is missing or invalid.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedHostel = await _tenentService.UpdateHostelAsync(Id, update, superAdminId);

            if (updatedHostel == null)
            {
                
                return NotFound(); 
            }

           
            return NoContent();
        }

        
    }
}
