using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.BankAccounts.AnyAsync())
            return;

        var account = new BankAccount("Test Account 1");

        context.BankAccounts.Add(account);

        await context.SaveChangesAsync();
    }
}