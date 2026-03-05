using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Testing.DomainTesting;

public class BankAccountTests(PostgresTestFixture fixture) : IntegrationTest(fixture)
{
    [Fact]
    public async Task GetSeededAccount_Should_ReturnOk()
    {
        // Arrange
        Guid id = new("11111111-1111-1111-1111-111111111111");

        // Act
        var account = await Context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
    }

    [Fact]
    public async Task AddMovementsToBankAccount_Should_ReturnOk()
    {
        // Arrange
        Guid id = new("11111111-1111-1111-1111-111111111111");

        var movement1 = new Movement((decimal)1.6, id, null);
        var movement2 = new Movement((decimal)4.7, id, null);

        // Act
        await Context.Movements.AddRangeAsync(movement1, movement2);
        await Context.SaveChangesAsync();

        var account = await Context.BankAccounts
            .Include(x => x.SourceMovements)
            .FirstOrDefaultAsync();

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
        Assert.Equal(2, account.SourceMovements.Count);

        Assert.Contains(account.SourceMovements, x => x.Id == movement1.Id);
        Assert.Contains(account.SourceMovements, x => x.Id == movement2.Id);
        Assert.Equal((decimal)6.3, account.SourceMovements.Sum(x => x.Amount));
    }

    [Fact]
    public async Task AddBankAccount_Should_Ok()
    {
        // Arrange
        var ba = new BankAccount(Guid.NewGuid(), "name1", 34);

        // Act
        Context.BankAccounts.Add(ba);
        await Context.SaveChangesAsync();

        var account = await Context.BankAccounts
            .FirstOrDefaultAsync(x => x.Id == ba.Id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal(ba.Id, account.Id);
        Assert.Equal("name1", account.Name);
        Assert.Equal(34, account.Balance);
    }
}
