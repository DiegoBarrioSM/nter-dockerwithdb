using Domain.Entities;

namespace Domain.IRepositories;

public interface IBankAccountRepository
{
    Task<BankAccount> GetByIdAsync(Guid id);

    void SaveBankAccount(BankAccount account);
}
