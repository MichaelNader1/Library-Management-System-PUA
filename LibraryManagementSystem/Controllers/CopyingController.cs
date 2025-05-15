using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CopyingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AdminUser> _userManager;

    public CopyingController(IUnitOfWork unitOfWork, UserManager<AdminUser> userManager)
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

        if (roles.Contains("Admin"))
        {
            var copyings = await _unitOfWork.Repository<Copying>()
                .GetAllAsync(include: q => q
                    .Include(c => c.LibraryBook)
                    .Include(c => c.PowerCampusUser));
            return Ok(copyings);
        }
        else
        {
            var copyings = await _unitOfWork.Repository<Copying>()
                .GetAllAsync(q => q
                    .Include(c => c.LibraryBook)
                    .Include(c => c.PowerCampusUser)
                    .Where(c => c.LibraryBook.LibraryID == user.LibraryID));
            return Ok(copyings);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        if (roles.Contains("Admin"))
        {
            var copyings = await _unitOfWork.Repository<Copying>()
                .GetAllAsync(q => q
                    .Include(c => c.LibraryBook)
                    .Where(c => c.User_Id == userId));
            return Ok(copyings);
        }
        else
        {
            var copyings = await _unitOfWork.Repository<Copying>()
                .GetAllAsync(q => q
                    .Include(c => c.LibraryBook)
                    .Where(c => c.User_Id == userId && c.LibraryBook.LibraryID == user.LibraryID));

            return Ok(copyings);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Copying copying)
    {
        if (copying == null)
            return BadRequest("Invalid copying data");

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var userExists = await _unitOfWork.Repository<PowerCampusUser>()
            .AnyAsync(u => u.User_Id == copying.User_Id);
        if (!userExists)
            return NotFound($"User with ID {copying.User_Id} does not exist in the view.");

        var libraryBook = await _unitOfWork.Repository<LibraryBook>()
            .GetByIdAsync(copying.LibraryBookID);
        if (libraryBook == null)
            return NotFound($"LibraryBook with ID {copying.LibraryBookID} does not exist.");

        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(libraryBook.BookID);
        if (book == null)
            return NotFound($"Book with ID {libraryBook.BookID} does not exist.");

        if (book.IsLocked)
            return BadRequest("This book is locked and cannot be copied.");

        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin") && libraryBook.LibraryID != user.LibraryID)
            return Unauthorized("You can only copy books from your own library.");

        if (libraryBook.Available_Copies <= 0)
            return BadRequest("No available copies to copy.");

        copying.Start_Time = DateTime.UtcNow;
        copying.IsReturned = false;

        await _unitOfWork.Repository<Copying>().AddAsync(copying);
        await _unitOfWork.SaveAsync();

        libraryBook.Available_Copies--;
        _unitOfWork.Repository<LibraryBook>().Update(libraryBook);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetByUserId), new { userId = copying.User_Id }, copying);
    }

    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var copying = await _unitOfWork.Repository<Copying>()
            .FindAsync(c => c.Copying_Id == id, include: q => q.Include(c => c.LibraryBook));

        if (copying == null)
            return NotFound("Copying record not found");

        if (copying.IsReturned)
            return BadRequest("Book already returned");

        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin") && copying.LibraryBook.LibraryID != user.LibraryID)
            return Unauthorized("You can only return copies from your own library.");

        copying.End_Time = DateTime.UtcNow;
        copying.IsReturned = true;

        copying.LibraryBook.Available_Copies++;
        _unitOfWork.Repository<LibraryBook>().Update(copying.LibraryBook);
        _unitOfWork.Repository<Copying>().Update(copying);

        await _unitOfWork.SaveAsync();
        return Ok("Copying has finished successfully.");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var copying = await _unitOfWork.Repository<Copying>()
            .FindAsync(c => c.Copying_Id == id, include: q => q.Include(c => c.LibraryBook));

        if (copying == null)
            return NotFound("Copying record not found");

        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin") && copying.LibraryBook.LibraryID != user.LibraryID)
            return Unauthorized("You can only delete copies from your own library.");

        if (!copying.IsReturned && copying.LibraryBook != null)
        {
            copying.LibraryBook.Available_Copies++;
            _unitOfWork.Repository<LibraryBook>().Update(copying.LibraryBook);
        }

        _unitOfWork.Repository<Copying>().Delete(copying);
        await _unitOfWork.SaveAsync();

        return Ok("Copying has been deleted successfully.");
    }


}
