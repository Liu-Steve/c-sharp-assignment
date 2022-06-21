using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace BusHelper.Context;

public class BusContext : DbContext
{
    public BusContext(DbContextOptions options)
    : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;database=busafer;uid=root;pwd=root;", 
            new MySqlServerVersion(new Version(5, 7, 36)));
        //optionsBuilder.UseMySQL("server=localhost;database=busafer;uid=root;pwd=root;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bus>();
    }
    
    public DbSet<Bus> Buses { get; set; }
}