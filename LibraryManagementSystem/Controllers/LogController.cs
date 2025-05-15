using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LogController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public LogController(LibraryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Log>>> GetAllLogs()
    {
        return await _context.Logs.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Log>> GetLogById(int id)
    {
        var log = await _context.Logs.FindAsync(id);
        if (log == null)
            return NotFound();
        return log;
    }

    [HttpGet("by-table/{tableName}")]
    public async Task<ActionResult<IEnumerable<Log>>> GetLogsByTable(string tableName)
    {
        var logs = await _context.Logs.Where(l => l.TableName == tableName).ToListAsync();
        return logs.Any() ? Ok(logs) : NotFound();
    }

    [HttpGet("by-user/{userId}")]
    public async Task<ActionResult<IEnumerable<Log>>> GetLogsByUser(string userName)
    {
        var logs = await _context.Logs.Where(l => l.UserName == userName).ToListAsync();
        return logs.Any() ? Ok(logs) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Log>> AddLog(Log log)
    {
        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLogById), new { id = log.LogId }, log);
    }
}
