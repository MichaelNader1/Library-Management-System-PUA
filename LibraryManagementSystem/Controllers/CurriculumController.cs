using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CurriculumsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CurriculumsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Curriculum>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var curriculums = await _unitOfWork.Repository<Curriculum>().GetAllAsync();

        var curriculum = curriculums
            .Where(c => c.Curriculum_ID == id)
            .Select(c => new
            {
                c.Curriculum_ID,
                c.Curriculum_Name,
                c.Department_ID,
                Department = new
                {
                    c.Department.Department_ID,
                    c.Department.Department_Name,
                    c.Department.College_ID
                }
            }).FirstOrDefault();

        if (curriculum == null)
            return NotFound("Curriculum not found");

        return Ok(curriculum);
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Curriculum curriculum)
    {
        var department = await _unitOfWork.Repository<Department>().GetByIdAsync(curriculum.Department_ID);
        if (department == null)
        {
            return BadRequest("Department not found. Please enter a valid Department_ID.");
        }
        curriculum.Department = department; 
        await _unitOfWork.Repository<Curriculum>().AddAsync(curriculum);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = curriculum.Curriculum_ID }, curriculum);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Curriculum curriculum)
    {
        if (id != curriculum.Curriculum_ID) return BadRequest("ID mismatch");

        var existingCurriculum = await _unitOfWork.Repository<Curriculum>().GetByIdAsync(id);
        if (existingCurriculum == null) return NotFound("Curriculum not found");

        _unitOfWork.Repository<Curriculum>().Update(curriculum);
        await _unitOfWork.SaveAsync();
        return Ok(curriculum);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var curriculum = await _unitOfWork.Repository<Curriculum>().GetByIdAsync(id);
        if (curriculum == null) return NotFound("Curriculum not found");

        _unitOfWork.Repository<Curriculum>().Delete(curriculum);
        await _unitOfWork.SaveAsync();
        return Ok("Curriculum Deleted Successfully");
    }
}
