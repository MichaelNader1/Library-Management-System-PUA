using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BorrowingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBorrowingService _borrowingService;
    private readonly IPenaltyService _penaltyService;
    private readonly IBannedUserService _bannedUserService;
    private readonly UserManager<AdminUser> _userManager;
    public BorrowingController(
        IUnitOfWork unitOfWork,
        IBorrowingService borrowingService,
        IPenaltyService penaltyService,
        IBannedUserService bannedUserService,
        UserManager<AdminUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _borrowingService = borrowingService;
        _penaltyService = penaltyService;
        _bannedUserService = bannedUserService;
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
            var borrowings = await _unitOfWork.Repository<Borrowing>()
                .GetAllAsync(q => q.Include(b => b.LibraryBook));
            return Ok(borrowings);
        }
        else
        {
            var borrowings = await _unitOfWork.Repository<Borrowing>()
                .GetAllAsync(q => q
                    .Include(b => b.LibraryBook)
                    .Where(b => b.LibraryBook.LibraryID == user.LibraryID));
            return Ok(borrowings);
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
            var borrowings = await _unitOfWork.Repository<Borrowing>()
                .GetAllAsync(q => q
                    .Include(b => b.LibraryBook)
                    .Where(b => b.User_Id == userId));
            return Ok(borrowings);
        }
        else
        {
            var borrowings = await _unitOfWork.Repository<Borrowing>()
                .GetAllAsync(q => q
                    .Include(b => b.LibraryBook)
                    .Where(b => b.User_Id == userId && b.LibraryBook.LibraryID == user.LibraryID));
            return Ok(borrowings);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Borrowing borrowing)
    {
        if (borrowing == null)
            return BadRequest("Invalid borrowing data");
        if (borrowing.NumberOfDays <= 0)
            return BadRequest("Number of days must be greater than 0");
        var user = await _userManager.GetUserAsync(User); 
        if (user == null)
            return Unauthorized();
        var userExists = await _unitOfWork.Repository<PowerCampusUser>()
            .AnyAsync(u => u.User_Id == borrowing.User_Id);
        if (!userExists)
            return NotFound($"User with ID {borrowing.User_Id} does not exist in the view.");
        var libraryBook = await _unitOfWork.Repository<LibraryBook>()
            .GetByIdAsync(borrowing.LibraryBookID);
        if (libraryBook == null)
            return NotFound($"LibraryBook with ID {borrowing.LibraryBookID} does not exist.");
        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin"))
        {
            if (libraryBook.LibraryID != user.LibraryID)
            {
                return Unauthorized("You can only borrow books from your own library.");
            }
        }
        try
        {
            await _borrowingService.AddBorrowingAsync(borrowing);
            return CreatedAtAction(nameof(GetByUserId), new { userId = borrowing.User_Id }, borrowing);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();
        var borrowing = await _unitOfWork.Repository<Borrowing>()
            .FindAsync(b => b.Borrowing_Id == id, include: q => q.Include(b => b.LibraryBook));
        if (borrowing == null)
            return NotFound("Borrowing record not found");
        if (borrowing.IsReturned)
            return BadRequest("Book already returned");
        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin"))
        {
            if (borrowing.LibraryBook.LibraryID != user.LibraryID)
            {
                return Unauthorized("You can only return books from your own library.");
            }
        }
        try
        {
            await _borrowingService.ReturnBookAsync(id);
            return Ok("Book returned successfully with penalty check and ban if needed.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();
        var borrowing = await _unitOfWork.Repository<Borrowing>()
            .FindAsync(b => b.Borrowing_Id == id, include: q => q.Include(b => b.LibraryBook));
        if (borrowing == null)
            return NotFound("Borrowing record not found");
        if (borrowing.IsReturned)
            return BadRequest("Cannot delete a completed borrowing record");
        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin"))
        {
            if (borrowing.LibraryBook.LibraryID != user.LibraryID)
            {
                return Unauthorized("You can only delete borrowings from your own library.");
            }
        }
        borrowing.LibraryBook.Available_Copies++;
        _unitOfWork.Repository<Borrowing>().Delete(borrowing);
        await _unitOfWork.SaveAsync();
        return Ok("Borrowing has been deleted successfully.");
    }
}