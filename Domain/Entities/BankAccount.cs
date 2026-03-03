namespace Domain.Entities;
public class BankAccount
{
    private BankAccount() { }

    public BankAccount(string name)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Balance = 0m;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public decimal Balance { get; private set; }
    
    public ICollection<Movement> SourceMovements { get; private set; } = [];

    public ICollection<Movement> TargetMovements { get; private set; } = [];
}
