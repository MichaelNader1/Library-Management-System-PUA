using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<AdminUser> _userManager;

    public UserController(UserManager<AdminUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("role")]
    public async Task<IActionResult> GetUserRole()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }

    [HttpGet("library")]
    public async Task<IActionResult> GetUserLibrary()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var libraryID = user.LibraryID; // Assuming your user has a `LibraryID` field
        return Ok(new { libraryID });
    }


}
