using Backend.Features.Clients;
using Backend.Features.Teams;
using Backend.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){ }

    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    //
    public DbSet<Client> Clients { get; set; }
}