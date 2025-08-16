using API.Context;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController(AppDbContext context) : ControllerBase    
{
    protected readonly AppDbContext _context = context;

}

