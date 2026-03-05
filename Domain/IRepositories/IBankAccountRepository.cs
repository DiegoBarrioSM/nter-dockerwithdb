using Domain.Entities;

namespace Domain.IRepositories;

public interface IBankAccountRepository
{
    Task<BankAccount> GetByIdAsync(Guid id);

    Task<Guid> AddBankAccountAsync(BankAccount account);
}
