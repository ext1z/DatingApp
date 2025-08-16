using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users{ get; set; }
}
