namespace backend.Models;

public class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AllowedCurrencies { get; set; } = string.Empty; // stored as "USD,EUR,RUB"
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation property
    public List<Payment> Payments { get; set; } = new();
}