using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagemantSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuetsController : ControllerBase
    {
        private readonly IGuestsServices _guestsService;
        public GuetsController(IGuestsServices guestsServices)
        {
                       _guestsService = guestsServices;
        }
        [HttpGet("guests")]
        public async Task<IActionResult> GetAllGuests()
        {
            int hostelId = 1; // later from JWT
            var result = await _guestsService.GetAllGuestsAsync(hostelId);
            return Ok(result);
        }
        [HttpPost("guests")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "HostelAdmin")]
        public async Task<IActionResult> RegisterGuest([FromForm] NewGuestsRegister dto)
        {
            var result = await _guestsService.RegisterGuestsAsync(dto);

            if (result)
                return Ok(new { success = true, message = "Guest registered successfully" });

            return BadRequest(new { success = false, message = "Guest registration failed" });
        }

        [HttpPut("guests/{guestId}")]
        public async Task<IActionResult> UpdateGuest(int guestId, [FromBody] UpdateGuest dto)
        {
            var result = await _guestsService.UpdateGuestsAsync(guestId, dto);
            if (result)
                return Ok(new { success = true, message = "Guest updated successfully" });

            return BadRequest(new { success = false, message = "Guest update failed" });
        }
        [HttpPatch("guests/{guestId}/status")]
        public async Task<IActionResult> ToggleGuestStatus(int guestId)
        {
            var result = await _guestsService.InActiveGuestsAsync(guestId);
            if (result)
                return Ok(new { success = true, message = "Guest status updated successfully" });

            return BadRequest(new { success = false, message = "Failed to update guest status" });
        }
    }
}
