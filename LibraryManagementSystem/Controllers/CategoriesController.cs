using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Category>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
        if (category == null) return NotFound("Category not found");
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Category category)
    {
        await _unitOfWork.Repository<Category>().AddAsync(category);
        await _unitOfWork.SaveAsync();
        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Category category)
    {
        if (id != category.Category_Id) return BadRequest("ID mismatch");

        _unitOfWork.Repository<Category>().Update(category);
        await _unitOfWork.SaveAsync();
        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
        if (category == null) return NotFound("Category not found");

        _unitOfWork.Repository<Category>().Delete(category);
        await _unitOfWork.SaveAsync();
        return Ok("Category Deleted Successfully");
    }
}
