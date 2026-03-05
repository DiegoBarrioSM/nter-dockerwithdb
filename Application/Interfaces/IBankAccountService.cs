using Application.DTO;

namespace Application.Interfaces;

public interface IBankAccountService
{
    Task<BankAccountDto?> GetByIdAsync(Guid id);

    Task<Guid?> AddAsync(BankAccountAddDto bankAccount);
}