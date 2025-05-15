namespace LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class VisitsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AdminUser> _userManager;

    public VisitsController(IUnitOfWork unitOfWork, UserManager<AdminUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        IEnumerable<Visits> visits;

        if (isAdmin)
        {
            visits = await _unitOfWork.Repository<Visits>()
                .GetAllAsync(q => q.Include(v => v.PowerCampusUser));
        }
        else
        {
            visits = await _unitOfWork.Repository<Visits>()
                .GetAllAsync(q => q
                    .Include(v => v.PowerCampusUser)
                    .Where(v => v.Library_Id == user.LibraryID));
        }

        return Ok(visits);
    }


    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        var visits = await _unitOfWork.Repository<Visits>()
            .GetAllAsync(q => q
                .Include(v => v.PowerCampusUser)
                .Where(v => (isAdmin || v.Library_Id == user.LibraryID) && v.User_Id == userId));

        if (!visits.Any())
            return NotFound("No visits found for this user");

        return Ok(visits);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Visits visit)
    {
        Console.WriteLine("Add Visit Called");
        if (visit == null || (visit.Visit_Type != "Study" && visit.Visit_Type != "Computer"))
            return BadRequest("Invalid visit type. Allowed values: 'Study' or 'Computer'");

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        var userExists = await _unitOfWork.Repository<PowerCampusUser>()
            .AnyAsync(u => u.User_Id == visit.User_Id);

        if (!userExists)
            return NotFound($"User with ID {visit.User_Id} does not exist in the view.");

        if (!isAdmin && visit.Library_Id != user.LibraryID)
            return StatusCode(403, "You are not authorized to add a reading for this library");

        visit.Visit_Date = DateTime.UtcNow;

        await _unitOfWork.Repository<Visits>().AddAsync(visit);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetByUserId), new { userId = visit.User_Id }, visit);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Visits visit)
    {
        if (id != visit.Visit_Id || (visit.Visit_Type != "Study" && visit.Visit_Type != "Computer"))
            return BadRequest("Invalid request or visit type");

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        var existingVisit = await _unitOfWork.Repository<Visits>().GetByIdAsync(id);

        if (existingVisit == null)
            return NotFound("Visit not found");

        if (!isAdmin && existingVisit.Library_Id != user.LibraryID)
            return StatusCode(403, "You are not authorized to add a reading for this library");

        existingVisit.Visit_Type = visit.Visit_Type;
        existingVisit.Visit_Date = visit.Visit_Date;

        _unitOfWork.Repository<Visits>().Update(existingVisit);
        await _unitOfWork.SaveAsync();

        return Ok("Visit has been updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        var visit = await _unitOfWork.Repository<Visits>().GetByIdAsync(id);

        if (visit == null)
            return NotFound("Visit not found");

        if (!isAdmin && visit.Library_Id != user.LibraryID)
            return StatusCode(403, "You are not authorized to add a reading for this library");

        _unitOfWork.Repository<Visits>().Delete(visit);
        await _unitOfWork.SaveAsync();

        return Ok("Visit has been deleted");
    }

}


