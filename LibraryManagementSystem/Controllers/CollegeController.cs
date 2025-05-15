using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CollegesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CollegesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _unitOfWork.Repository<College>().GetAllAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var colleges = await _unitOfWork.Repository<College>()
                .GetAllAsync(q => q.Where(c => c.College_ID == id)
                    .Include(c => c.Departments)
                        .ThenInclude(d => d.Curriculums));

            var college = colleges
                .Select(c => new
                {
                    c.College_ID,
                    c.College_Name,
                    departments = c.Departments.Select(d => new
                    {
                        d.Department_ID,
                        d.Department_Name,
                        d.College_ID,
                        curriculums = d.Curriculums.Select(cr => new
                        {
                            cr.Curriculum_ID,
                            cr.Curriculum_Name,
                            cr.Department_ID
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefault();

            if (college == null)
                return NotFound("College not found");

            return Ok(college);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }




    [HttpPost]
    public async Task<IActionResult> Add([FromBody] College college)
    {
        try
        {
            await _unitOfWork.Repository<College>().AddAsync(college);
            await _unitOfWork.SaveAsync();
            return Ok(college);
    }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
}
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] College college)
    {
        try
        {
            if (id != college.College_ID) return BadRequest("ID mismatch");
            _unitOfWork.Repository<College>().Update(college);
            await _unitOfWork.SaveAsync();
            return Ok(college);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var college = await _unitOfWork.Repository<College>().GetByIdAsync(id);
            if (college == null) return NotFound("College not found");

            _unitOfWork.Repository<College>().Delete(college);
            await _unitOfWork.SaveAsync();
            return Ok("College Deleted Successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
