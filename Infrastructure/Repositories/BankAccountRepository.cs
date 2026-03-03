using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using System.Data.Entity;

namespace Insfrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly AppDbContext _dbContext;

    public BankAccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BankAccount> GetByIdAsync(Guid id)
    {
        return await _dbContext.BankAccounts
            .Include(b => b.SourceMovements)
            .Include(b => b.TargetMovements)
            .FirstAsync(x => x.Id == id);
    }

    public void SaveBankAccount(BankAccount account)
    {
        _dbContext.BankAccounts.Add(account);
        _dbContext.SaveChanges();
    }
}
