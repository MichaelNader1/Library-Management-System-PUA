using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Department>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var departments = await _unitOfWork.Repository<Department>().GetAllAsync();

        var department = departments
            .Where(d => d.Department_ID == id)
            .Select(d => new
            {
                d.Department_ID,
                d.Department_Name,
                d.College_ID,
                Curriculums = d.Curriculums.Select(c => new
                {
                    c.Curriculum_ID,
                    c.Curriculum_Name,
                    c.Department_ID
                }).ToList()
            }).FirstOrDefault();

        if (department == null)
            return NotFound("Department not found");

        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Department department)
    {
        if (department == null)
            return BadRequest("Invalid department data");

        await _unitOfWork.Repository<Department>().AddAsync(department);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = department.Department_ID }, department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Department department)
    {
        if (id != department.Department_ID) return BadRequest("ID mismatch");
        _unitOfWork.Repository<Department>().Update(department);
        await _unitOfWork.SaveAsync();
        return Ok(department);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _unitOfWork.Repository<Department>().GetByIdAsync(id);
        if (department == null) return NotFound("Department not found");

        _unitOfWork.Repository<Department>().Delete(department);
        await _unitOfWork.SaveAsync();
        return Ok("Department Deleted Successfully");
    }
}
