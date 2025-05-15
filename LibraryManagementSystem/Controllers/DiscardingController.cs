using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiscardingController : ControllerBase
    {
        private readonly IDiscardingService _discardingService;
        private readonly UserManager<AdminUser> _userManager;

        public DiscardingController(IDiscardingService discardingService, UserManager<AdminUser> userManager)
        {
            _discardingService = discardingService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Discarding discarding)
        {
            if (discarding == null)
            {
                return BadRequest("Invalid request.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
            {
                return Forbid(); // Return a 403 Forbidden response for non-admin users
            }

            try
            {
                await _discardingService.DiscardBookAsync(discarding.LibraryBookID, discarding.Discarding_Reason);
                return Ok("Book discarded successfully.");
            }
            catch (Exception ex)
            {
                // Log the full exception for debugging
                Console.WriteLine($"Error occurred: {ex.Message}");
                return BadRequest(new { message = "An error occurred while saving the entity changes. See the inner exception for details.", innerException = ex.InnerException?.Message });
            }

        }
    }
}
