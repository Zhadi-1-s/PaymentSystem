using backend.Data;
using backend.DTO;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class PaymentService
{
    private readonly AppDbContext _db;

    // AppDbContext is injected automatically — like NestJS constructor injection
    public PaymentService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
    {
        var payment = new Payment
        {
            WalletNumber = request.WalletNumber,
            Account      = request.Account,
            Email        = request.Email,
            Phone        = request.Phone,
            Amount       = request.Amount,
            Currency     = request.Currency,
            Comment      = request.Comment,
            Status       = PaymentStatus.Created   // always Created for now
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