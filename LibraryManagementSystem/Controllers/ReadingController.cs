using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReadingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AdminUser> _userManager;

    public ReadingController(IUnitOfWork unitOfWork, UserManager<AdminUser> userManager)
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

        IEnumerable<Reading> readings;

        if (isAdmin)
        {
            readings = await _unitOfWork.Repository<Reading>()
                .GetAllAsync(q => q.Include(r => r.LibraryBook));
        }
        else
        {
            readings = await _unitOfWork.Repository<Reading>()
                .GetAllAsync(q => q
                    .Include(r => r.LibraryBook)
                    .Where(r => r.LibraryBook.LibraryID == user.LibraryID));
        }

        return Ok(readings);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        IEnumerable<Reading> readings;

        if (isAdmin)
        {
            readings = await _unitOfWork.Repository<Reading>()
                .GetAllAsync(q => q
                    .Include(r => r.LibraryBook)
                    .Where(r => r.User_Id == userId));
        }
        else
        {
            readings = await _unitOfWork.Repository<Reading>()
                .GetAllAsync(q => q
                    .Include(r => r.LibraryBook)
                    .Where(r => r.User_Id == userId && r.LibraryBook.LibraryID == user.LibraryID));
        }

        if (!readings.Any())
            return NotFound("No readings found for this user");

        return Ok(readings);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Reading reading)
    {
        if (reading == null || reading.User_Id == null || reading.LibraryBookID == 0)
            return BadRequest("Invalid reading data");

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        var userExists = await _unitOfWork.Repository<PowerCampusUser>()
            .AnyAsync(u => u.User_Id == reading.User_Id);

        if (!userExists)
            return NotFound($"User with ID {reading.User_Id} does not exist in the view.");

        var libraryBook = await _unitOfWork.Repository<LibraryBook>()
            .GetByIdAsync(reading.LibraryBookID);

        if (libraryBook == null)
            return NotFound($"LibraryBook with ID {reading.LibraryBookID} not found");
        
        if (!isAdmin && libraryBook.LibraryID != user.LibraryID)
            return StatusCode(403, "You are not authorized to add a reading for this library");

        var book = await _unitOfWork.Repository<Book>()
            .GetByIdAsync(libraryBook.BookID);

        if (book == null)
            return NotFound($"Book with ID {libraryBook.BookID} not found");

        if (book.IsLocked)
            return BadRequest($"Book '{book.Title}' is locked and cannot be read");

        if (libraryBook.Available_Copies <= 0)
            return BadRequest($"No available copies for '{book.Title}'");

        reading.Start_Time = DateTime.UtcNow;
        reading.IsFinished = false;

        await _unitOfWork.Repository<Reading>().AddAsync(reading);
        libraryBook.Available_Copies--;
        _unitOfWork.Repository<LibraryBook>().Update(libraryBook);

        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetByUserId), new { userId = reading.User_Id }, reading);
    }

    [HttpPut("{id}/finish")]
    public async Task<IActionResult> FinishReading(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var reading = await _unitOfWork.Repository<Reading>()
            .FindAsync(r => r.Reading_Id == id, include: q => q.Include(r => r.LibraryBook));

        if (reading == null)
            return NotFound("Reading record not found");

        if (reading.IsFinished)
            return BadRequest("Reading already finished");

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        if (!isAdmin && (reading.LibraryBook == null || reading.LibraryBook.LibraryID != user.LibraryID))
            return Unauthorized("You can only finish readings for your own library.");

        reading.End_Time = DateTime.UtcNow;
        reading.IsFinished = true;

        if (reading.LibraryBook != null)
        {
            reading.LibraryBook.Available_Copies++;
            _unitOfWork.Repository<LibraryBook>().Update(reading.LibraryBook);
        }

        _unitOfWork.Repository<Reading>().Update(reading);
        await _unitOfWork.SaveAsync();

        return Ok("Reading has finished");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var reading = await _unitOfWork.Repository<Reading>()
            .FindAsync(r => r.Reading_Id == id, include: q => q.Include(r => r.LibraryBook));

        if (reading == null)
            return NotFound("Reading record not found");

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");

        if (!isAdmin && (reading.LibraryBook == null || reading.LibraryBook.LibraryID != user.LibraryID))
            return Unauthorized("You can only delete readings for your own library.");

        if (reading.LibraryBook != null)
        {
            reading.LibraryBook.Available_Copies++;
            _unitOfWork.Repository<LibraryBook>().Update(reading.LibraryBook);
        }

        _unitOfWork.Repository<Reading>().Delete(reading);
        await _unitOfWork.SaveAsync();

        return Ok("Reading has been deleted");
    }
}
