using System.Security.Cryptography;
using System.Text;
using API.Context;
using API.DTOs;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class AccountController(AppDbContext context, ITokenService _tokenService) : BaseController(context)
{
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> RegisterAsync([FromBody] RegisterDto request)
    {   
        if(await EmailExistAsync(request.Email, request.DisplayName)) return BadRequest("User with this email or with this username already exist");

        using var hmac = new HMACSHA512();

        var user = new AppUser(){
            DisplayName = request.DisplayName,
            Email = request.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AppUser>> LoginAsync([FromBody] LoginDto request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        if(user == null) return Unauthorized("Invalid email");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        // TODO change logic to safety logic(DeepSeek)
        for (var i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return Ok(new UserDto {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
        });
    }


    private async Task<bool> EmailExistAsync(string email, string userName)
    {
        return await _context.Users.AnyAsync(
            user => user.Email.ToLower() == email.ToLower() ||
             user.DisplayName == userName.ToLower());
    }
}

