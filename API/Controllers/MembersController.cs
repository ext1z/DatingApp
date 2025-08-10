using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController(AppDbContext appDbContext) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetMembersAsync() => Ok(await appDbContext.Users.ToListAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetMemberByIdAsync([FromRoute] string id)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();

        return Ok(user);
    }
}

