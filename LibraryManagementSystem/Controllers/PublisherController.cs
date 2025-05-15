using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PublishersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Publisher>().GetAllAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var publisher = await _unitOfWork.Repository<Publisher>().GetByIdAsync(id);
        if (publisher == null) return NotFound("publisher not found");
        return Ok(publisher);
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Publisher publisher)
    {
        await _unitOfWork.Repository<Publisher>().AddAsync(publisher);
        await _unitOfWork.SaveAsync();
        return Ok(publisher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Publisher publisher)
    {
        if (id != publisher.Publisher_ID) return BadRequest("ID mismatch");
        _unitOfWork.Repository<Publisher>().Update(publisher);
        await _unitOfWork.SaveAsync();
        return Ok(publisher);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var publisher = await _unitOfWork.Repository<Publisher>().GetByIdAsync(id);
        if (publisher == null) return NotFound("Publisher not found");

        _unitOfWork.Repository<Publisher>().Delete(publisher);
        await _unitOfWork.SaveAsync();
        return Ok("Publisher Deleted Successfully");
    }
}
