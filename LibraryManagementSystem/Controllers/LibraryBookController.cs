using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LibraryBookController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AdminUser> _userManager;

    public LibraryBookController(IUnitOfWork unitOfWork, UserManager<AdminUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var allLibraryBooks = await _unitOfWork.Repository<LibraryBook>().GetAllAsync();

        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return Ok(allLibraryBooks);
        }
        else
        {
            var filtered = allLibraryBooks
                .Where(lb => lb.LibraryID == user.LibraryID)
                .ToList();

            return Ok(filtered);
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var libraryBook = await _unitOfWork.Repository<LibraryBook>().FindAsync(
            lb => lb.LibraryBookID == id
        );

        if (libraryBook == null) return NotFound("Library Book not found");

        if (!await _userManager.IsInRoleAsync(user, "Admin"))
        {
            if (libraryBook.LibraryID != user.LibraryID)
                return StatusCode(403, "You are not authorized to add a reading for this library");
        }

        return Ok(new
        {
            libraryBook.LibraryBookID,
            libraryBook.BookID,
            libraryBook.LibraryID,
            libraryBook.Total_Copies,
            libraryBook.Available_Copies
        });
    }

    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] LibraryBook libraryBook)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            await _unitOfWork.Repository<LibraryBook>().AddAsync(libraryBook);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            if (libraryBook.LibraryID != user.LibraryID)
            {
                return Unauthorized("You can only add books to your own library.");
            }

            await _unitOfWork.Repository<LibraryBook>().AddAsync(libraryBook);
            await _unitOfWork.SaveAsync();
        }

        var existingRelation = await _unitOfWork.Repository<BookLibraryBook>()
            .FindAsync(blb => blb.BookID == libraryBook.BookID && blb.LibraryBookID == libraryBook.LibraryBookID);

        if (existingRelation == null)
        {
            var bookLibraryBook = new BookLibraryBook
            {
                BookID = libraryBook.BookID,
                LibraryBookID = libraryBook.LibraryBookID
            };

            await _unitOfWork.Repository<BookLibraryBook>().AddAsync(bookLibraryBook);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        return CreatedAtAction(nameof(GetById), new { id = libraryBook.LibraryBookID }, libraryBook);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LibraryBook libraryBook)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var existingLibraryBook = await _unitOfWork.Repository<LibraryBook>().GetByIdAsync(id);
        if (existingLibraryBook == null) return NotFound("Library Book not found");

        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            _unitOfWork.Repository<LibraryBook>().Update(libraryBook);
            await _unitOfWork.SaveAsync();
            return Ok(libraryBook);
        }
        else
        {
            if (existingLibraryBook.LibraryID != user.LibraryID)
            {
                return Unauthorized("You can only update books from your own library.");
            }

            _unitOfWork.Repository<LibraryBook>().Update(libraryBook);
            await _unitOfWork.SaveAsync();
            return Ok(libraryBook);
        }
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var libraryBook = await _unitOfWork.Repository<LibraryBook>().GetByIdAsync(id);
        if (libraryBook == null) return NotFound("Library Book not found");

        var user = await _userManager.GetUserAsync(User);
        var userLibraryId = user.LibraryID;

        if (libraryBook.LibraryID != userLibraryId)
        {
            return Unauthorized("You can only delete books from your own library.");
        }

        _unitOfWork.Repository<LibraryBook>().Delete(libraryBook);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
