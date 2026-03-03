using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BankAccount> BankAccounts { get; set; }

    public DbSet<Movement> Movements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movement>()
            .HasOne(m => m.SourceBankAccount)
            .WithMany(b => b.SourceMovements)
            .HasForeignKey(m => m.SourceAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movement>()
            .HasOne(m => m.TargetBankAccount)
            .WithMany(b => b.TargetMovements)
            .HasForeignKey(m => m.TargetAccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
