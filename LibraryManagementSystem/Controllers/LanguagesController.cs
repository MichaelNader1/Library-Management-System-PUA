using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LanguagesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public LanguagesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Language>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var language = await _unitOfWork.Repository<Language>().FindAsync(
            l => l.Language_Id == id
        );

        if (language == null)
            return NotFound("Language not found");

        return Ok(new
        {
            language.Language_Id,
            language.Language_Name
        });
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Language language)
    {
        await _unitOfWork.Repository<Language>().AddAsync(language);
        await _unitOfWork.SaveAsync();
        return Ok(language);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Language language)
    {
        if (id != language.Language_Id) return BadRequest("ID mismatch");

        _unitOfWork.Repository<Language>().Update(language);
        await _unitOfWork.SaveAsync();
        return Ok(language);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var language = await _unitOfWork.Repository<Language>().GetByIdAsync(id);
        if (language == null) return NotFound("Language not found");

        _unitOfWork.Repository<Language>().Delete(language);
        await _unitOfWork.SaveAsync();
        return Ok("Language Deleted Successfully");
    }
}
