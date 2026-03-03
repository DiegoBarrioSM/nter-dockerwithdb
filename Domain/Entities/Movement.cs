namespace Domain.Entities;

public class Movement
{
    private Movement() { }

    public Movement(decimal amount, Guid sourceAccountId, Guid? targetAccountId)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        SourceAccountId = sourceAccountId;
        TargetAccountId = targetAccountId;
    }

    public Guid Id { get; private set; }

    public decimal Amount { get; private set; }

    public Guid SourceAccountId { get; private set; }
    public BankAccount SourceBankAccount { get; private set; } = null!;

    public Guid? TargetAccountId { get; private set; }
    public BankAccount? TargetBankAccount { get; private set; }
}
