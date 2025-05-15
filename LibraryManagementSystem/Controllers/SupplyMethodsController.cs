using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SupplyMethodsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SupplyMethodsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Repository<Supply_Method>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var SupplyMethod = await _unitOfWork.Repository<Supply_Method>().GetByIdAsync(id);
        if (SupplyMethod == null) return NotFound("Supply Method not found");
        return Ok(SupplyMethod);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Supply_Method supplyMethod)
    {
        await _unitOfWork.Repository<Supply_Method>().AddAsync(supplyMethod);
        await _unitOfWork.SaveAsync();
        return Ok(supplyMethod);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Supply_Method supplyMethod)
    {
        if (id != supplyMethod.Supply_Method_Id) return BadRequest("ID mismatch");

        _unitOfWork.Repository<Supply_Method>().Update(supplyMethod);
        await _unitOfWork.SaveAsync();
        return Ok(supplyMethod);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var supplyMethod = await _unitOfWork.Repository<Supply_Method>().GetByIdAsync(id);
        if (supplyMethod == null) return NotFound("Supply Method not found");

        _unitOfWork.Repository<Supply_Method>().Delete(supplyMethod);
        await _unitOfWork.SaveAsync();
        return Ok("Supply Method Deleted Successfully");
    }
}
