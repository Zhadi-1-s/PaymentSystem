namespace backend.Models;

public enum PaymentStatus
{
    Created,
    Rejected
}

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string WalletNumber { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }         // ? means optional, like TypeScript
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; } = PaymentStatus.Created;
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }

}