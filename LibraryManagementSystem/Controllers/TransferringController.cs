using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] // Enforce Admin-only access to the whole controller (optional)
public class TransferringController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransferringController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferBook([FromBody] Transferring transferring)
    {
        try
        {
            await _transferService.TransferBookAsync(
                transferring.SourceLibraryBookID,
                transferring.DestinationLibraryBookID
            );
            return Ok("Book transferred successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
