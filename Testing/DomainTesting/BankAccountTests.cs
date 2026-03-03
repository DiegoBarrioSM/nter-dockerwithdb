using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Testing.DomainTesting;

public class BankAccountTests : IClassFixture<PostgresTestFixture>
{
    private readonly DbContextOptions<AppDbContext> _options;

    public BankAccountTests(PostgresTestFixture fixture)
    {
        _options = fixture.DbOptions;
    }

    [Fact]
    public async Task Should_Get_Seeded_Account()
    {
        await using var context = new AppDbContext(_options);

        var account = await context.BankAccounts.FirstOrDefaultAsync();

        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
    }

    [Fact]
    public async Task BankAccount_Related_Two_Movement()
    {
        await using var context = new AppDbContext(_options);

        var account = await context.BankAccounts.FirstOrDefaultAsync();

        var movement1 = new Movement((decimal)1.6, account!.Id, null);
        var movement2 = new Movement((decimal)4.7, account!.Id, null);

        await context.Movements.AddRangeAsync(movement1, movement2);
        await context.SaveChangesAsync();

        account = await context.BankAccounts
            .Include(x => x.SourceMovements)
            .FirstOrDefaultAsync();

        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
        Assert.Equal(2, account.SourceMovements.Count);

        Assert.Contains(account.SourceMovements, x => x.Id == movement1.Id);
        Assert.Contains(account.SourceMovements, x => x.Id == movement2.Id);
        Assert.Equal((decimal)6.3, account.SourceMovements.Sum(x => x.Amount));
    }
}
