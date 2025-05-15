using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthorsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Author>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _unitOfWork.Repository<Author>().GetByIdAsync(id);
        if (author == null) return NotFound("Author not found");
        return Ok(author);
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Author author)
    {
        await _unitOfWork.Repository<Author>().AddAsync(author);
        await _unitOfWork.SaveAsync();
        return Ok(author);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Author author)
    {
        if (id != author.Author_ID) return BadRequest("ID mismatch");

        _unitOfWork.Repository<Author>().Update(author);
        await _unitOfWork.SaveAsync();
        return Ok(author);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _unitOfWork.Repository<Author>().GetByIdAsync(id);
        if (author == null) return NotFound("Author not found");

        _unitOfWork.Repository<Author>().Delete(author);
        await _unitOfWork.SaveAsync();
        return Ok("Author Deleted Successfully");
    }
}
