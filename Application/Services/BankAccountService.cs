using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _repository;

    public BankAccountService(IBankAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<BankAccountDto?> GetByIdAsync(Guid id)
    {
        var account = await _repository.GetByIdAsync(id);

        if (account == null)
            return null;

        return new BankAccountDto
        {
            Id = account.Id,
            Name = account.Name,
            Balance = account.Balance,
            SourceMovements = [.. account.SourceMovements.Select(x => new MovementDto
            {
                Id = x.Id,
                Amount = x.Amount
            })],
            TargetMovements = [.. account.TargetMovements.Select(x => new MovementDto
            {
                Id = x.Id,
                Amount = x.Amount
            })],
        };
    }

    public async Task<Guid?> AddAsync(BankAccountAddDto dto)
    {
        var newBankAccount = new BankAccount(dto.Name, dto.Balance);

        var id = await _repository.AddBankAccountAsync(newBankAccount);

        return id;
    }
}