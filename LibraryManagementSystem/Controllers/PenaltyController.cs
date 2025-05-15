using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PenaltyController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AdminUser> _userManager;

    public PenaltyController(IUnitOfWork unitOfWork, UserManager<AdminUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    private async Task<bool> IsAdmin(AdminUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.Contains("Admin");
    }

    private async Task<AdminUser> GetCurrentUserAsync()
    {
        return await _userManager.FindByNameAsync(User.Identity!.Name!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = await GetCurrentUserAsync();
        if (await IsAdmin(user))
        {
            var all = await _unitOfWork.Repository<Penalty>()
                .GetAllAsync(include: p => p.Include(p => p.Borrowing));
            return Ok(all);
        }

        var penalties = await _unitOfWork.Repository<Penalty>().GetAllAsync(
            include: p => p.Include(p => p.Borrowing)
        );

        var filtered = penalties
            .Where(p => p.Borrowing != null && p.Borrowing.LibraryBook.LibraryID == user.LibraryID)
            .ToList();

        return Ok(filtered);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var penalty = await _unitOfWork.Repository<Penalty>().FindAsync(
            p => p.Penalty_Id == id,
            include: p => p.Include(p => p.Borrowing)
        );

        if (penalty == null)
            return NotFound("Penalty not found.");

        var user = await GetCurrentUserAsync();
        if (await IsAdmin(user) || (penalty.Borrowing?.LibraryBook?.LibraryID == user.LibraryID))
            return Ok(penalty);

        return StatusCode(403, "You are not authorized to add a reading for this library");
    }

    [HttpPost]
    public async Task<IActionResult> AddPenaltyManually([FromBody] Penalty penalty)
    {
        var user = await GetCurrentUserAsync();
        var borrowing = await _unitOfWork.Repository<Borrowing>().FindAsync(
            b => b.Borrowing_Id == penalty.Borrowing_Id,
            include: b => b.Include(b => b.LibraryBook)
        );

        if (borrowing == null)
            return BadRequest("Invalid Borrowing ID.");

        if (!await IsAdmin(user) && borrowing.LibraryBook.LibraryID != user.LibraryID)
            return StatusCode(403, "You are not authorized to add a reading for this library");

        penalty.Penalty_Date = DateTime.Now;
        await _unitOfWork.Repository<Penalty>().AddAsync(penalty);
        await _unitOfWork.SaveAsync();

        return Ok("Penalty added successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Penalty updatedPenalty)
    {
        var user = await GetCurrentUserAsync();
        var penalty = await _unitOfWork.Repository<Penalty>().FindAsync(
            p => p.Penalty_Id == id,
            include: p => p.Include(p => p.Borrowing).ThenInclude(b => b.LibraryBook)
        );

        if (penalty == null)
            return NotFound("Penalty not found.");

        if (!await IsAdmin(user) && penalty.Borrowing?.LibraryBook?.LibraryID != user.LibraryID)
            return StatusCode(403, "You are not authorized to add a reading for this library");

        penalty.Amount = updatedPenalty.Amount;
        penalty.Borrowing_Id = updatedPenalty.Borrowing_Id;

        _unitOfWork.Repository<Penalty>().Update(penalty);
        await _unitOfWork.SaveAsync();

        return Ok("Penalty updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await GetCurrentUserAsync();
        var penalty = await _unitOfWork.Repository<Penalty>().FindAsync(
            p => p.Penalty_Id == id,
            include: p => p.Include(p => p.Borrowing).ThenInclude(b => b.LibraryBook)
        );

        if (penalty == null)
            return NotFound("Penalty not found.");

        if (!await IsAdmin(user) && penalty.Borrowing?.LibraryBook?.LibraryID != user.LibraryID)
            return StatusCode(403, "You are not authorized to add a reading for this library");

        _unitOfWork.Repository<Penalty>().Delete(penalty);
        await _unitOfWork.SaveAsync();

        return Ok("Penalty deleted successfully.");
    }
}
