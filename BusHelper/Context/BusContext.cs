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
        optionsBuilder.UseMySql("server=119.13.81.183;database=safengine;uid=root;pwd=root;",
            new MySqlServerVersion(new Version(8, 0, 29)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bus>();
    }

    public DbSet<Bus> Buses { get; set; }

    public DbSet<Call> Calls { get; set; }

    public DbSet<DangerAction> DangerActions { get; set; }

    public DbSet<DangerIndex> DangerIndices { get; set; }

    public DbSet<DangerRecord> DangerRecords { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    public DbSet<LeavingMsg> LeavingMsgs { get; set; }

    public DbSet<ManagerMsg> ManagerMsgs { get; set; }

    public DbSet<Manager> Managers { get; set; }

    public DbSet<RealTimeRecord> RealTimeRecords { get; set; }

    public DbSet<Road> Roads { get; set; }

    public DbSet<WorkInfo> WorkInfos { get; set; }

    public DbSet<Alert> Alerts { get; set; }
}