using backend.Data;
using backend.DTO;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class PaymentService
{
    private readonly AppDbContext _db;

    public PaymentService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request,Guid userId)
    {
        var user = await _db.Users.FindAsync(userId);

       var payment = new Payment
        {
            WalletNumber = request.WalletNumber,
            Account      = request.Account,
            Email        = user?.Email ?? request.Email,   // prefer user's email
            Phone        = user?.Phone ?? request.Phone,   // prefer user's phone
            Amount       = request.Amount,
            Currency     = request.Currency,
            Comment      = request.Comment,
            Status       = PaymentStatus.Created,
            UserId       = userId                          //  link to user
        };

        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();              // like repository.save()

        return MapToResponse(payment);
    }

    public async Task<List<PaymentResponse>> GetAllPaymentsAsync()
    {
        var payments = await _db.Payments
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return payments.Select(MapToResponse).ToList();
    }

    public async Task<List<PaymentResponse>> GetUserPaymentsAsync(Guid userId)
    {
        var payments = await _db.Payments
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return payments.Select(MapToResponse).ToList();
    }

    // Private helper — keeps mapping in one place
    private static PaymentResponse MapToResponse(Payment p) => new()
        {
            Id           = p.Id,
            WalletNumber = p.WalletNumber,
            Account      = p.Account,
            Email        = p.Email,
            Phone        = p.Phone,
            Amount       = p.Amount,
            Currency     = p.Currency,
            Status       = p.Status.ToString(),
            Comment      = p.Comment,
            CreatedAt    = p.CreatedAt
        };
        public async Task<StatsResponse> GetStatsAsync()
    {
        var payments = await _db.Payments.ToListAsync();

        var daily = payments
            .GroupBy(p => p.CreatedAt.Date)
            .Select(g => new DailyStats
            {
                Date  = g.Key.ToString("yyyy-MM-dd"),
                Count = g.Count(),
                Total = g.Sum(p => p.Amount)
            })
            .OrderByDescending(d => d.Date)
            .ToList();

        return new StatsResponse
        {
            TotalAmount       = payments.Sum(p => p.Amount),
            TotalTransactions = payments.Count,
            DailyBreakdown    = daily
        };
    }
}