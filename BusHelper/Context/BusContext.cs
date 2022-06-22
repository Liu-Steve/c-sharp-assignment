using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using BusHelper.Models;

namespace BusHelper.Context;

public class BusContext : DbContext
{
    public BusContext(DbContextOptions options)
    : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=10.132.0.233;database=busafer;uid=root;pwd=root;", 
            new MySqlServerVersion(new Version(8, 0, 29)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bus>();
    }
    
    public DbSet<Bus> Buses { get; set; }
}