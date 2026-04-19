using System.ComponentModel.DataAnnotations;

namespace backend.DTO;

public class CreatePaymentRequest
{
    [Required]
    public string WalletNumber { get; set; } = string.Empty;

    [Required]
    public string Account { get; set; } = string.Empty;

    [Required]
    [EmailAddress]                          // built-in email format check
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
    public decimal Amount { get; set; }

    [Required]
    public string Currency { get; set; } = string.Empty;

    public string? Comment { get; set; }
}

public class PaymentResponse
{
    public Guid Id { get; set; }
    public string WalletNumber { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class DailyStats
{
    public string Date { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Total { get; set; }
}

public class StatsResponse
{
    public decimal TotalAmount { get; set; }
    public int TotalTransactions { get; set; }
    public List<DailyStats> DailyBreakdown { get; set; } = new();
}
