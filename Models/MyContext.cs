#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace darts.Models;
public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; } 
    public DbSet<Team> Teams { get; set; } 
    public DbSet<Player> Players { get; set; } 
  //add models here if needed
}