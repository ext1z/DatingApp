using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Context;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class MembersController(AppDbContext context) : BaseController(context)
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetMembersAsync() => Ok(await _context.Users.ToListAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetMemberByIdAsync([FromRoute] string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();

        return Ok(user);
    }
}

