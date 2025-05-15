using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubheadingsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SubheadingsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Subheading>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var subheading = await _unitOfWork.Repository<Subheading>().FindAsync(
            s => s.Subheading_Id == id
        );

        if (subheading == null)
            return NotFound("Subheading not found");

        return Ok(new
        {
            subheading.Subheading_Id,
            subheading.Subheading_Name
        });
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Subheading subheading)
    {
        await _unitOfWork.Repository<Subheading>().AddAsync(subheading);
        await _unitOfWork.SaveAsync();
        return Ok(subheading);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Subheading subheading)
    {
        if (id != subheading.Subheading_Id) return BadRequest("ID mismatch");

        _unitOfWork.Repository<Subheading>().Update(subheading);
        await _unitOfWork.SaveAsync();
        return Ok(subheading);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var subheading = await _unitOfWork.Repository<Subheading>().GetByIdAsync(id);
        if (subheading == null) return NotFound("Subheading not found");

        _unitOfWork.Repository<Subheading>().Delete(subheading);
        await _unitOfWork.SaveAsync();
        return Ok("Subheading Deleted Successfully");
    }
}
