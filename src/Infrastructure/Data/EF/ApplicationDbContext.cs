using Core.Domain.Entities;
using Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EF;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
}