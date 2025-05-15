using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LibraryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AdminUser> _userManager;

    public LibraryController(IUnitOfWork unitOfWork, UserManager<AdminUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            var allLibraries = await _unitOfWork.Repository<Library>().GetAllAsync();
            var result = allLibraries.Select(lib => new
            {
                lib.LibraryID,
                lib.Name
            });
            return Ok(result);
        }
        else
        {
            var userLibrary = await _unitOfWork.Repository<Library>()
                .FindAsync(lib => lib.LibraryID == user.LibraryID);

            if (userLibrary == null)
                return NotFound("Library not found");

            return Ok(new[]
            {
                new
                {
                    userLibrary.LibraryID,
                    userLibrary.Name
                }
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var library = await _unitOfWork.Repository<Library>().GetByIdAsync(id);
        if (library == null) return NotFound("Library not found");

        return Ok(library);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Library library)
    {
        await _unitOfWork.Repository<Library>().AddAsync(library);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetById), new { id = library.LibraryID }, library);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Library library)
    {
        if (id != library.LibraryID)
            return BadRequest("ID mismatch");

        var existingLibrary = await _unitOfWork.Repository<Library>().GetByIdAsync(id);
        if (existingLibrary == null)
            return NotFound("Library not found");

        existingLibrary.Name = library.Name;

        _unitOfWork.Repository<Library>().Update(existingLibrary);
        await _unitOfWork.SaveAsync();

        return Ok(existingLibrary);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var library = await _unitOfWork.Repository<Library>().GetByIdAsync(id);
        if (library == null) return NotFound("Library not found");

        _unitOfWork.Repository<Library>().Delete(library);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}

