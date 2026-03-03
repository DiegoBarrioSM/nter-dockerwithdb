using Application.DTO;

namespace Application.Interfaces;

public interface IBankAccountService
{
    Task<BankAccountDto?> GetByIdAsync(Guid id);
}