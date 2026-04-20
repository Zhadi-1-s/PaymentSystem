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

    public async Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request, Guid userId)
    {
        var user    = await _db.Users.FindAsync(userId);
        var company = await _db.Companies.FindAsync(request.CompanyId);

        // Validate against company rules
        var status = PaymentStatus.Created;

        if (company == null)
        {
            status = PaymentStatus.Rejected;
        }
        else
        {
            var allowedCurrencies = company.AllowedCurrencies.Split(',');

            if (!allowedCurrencies.Contains(request.Currency))
                status = PaymentStatus.Rejected;

            if (request.Amount < company.MinAmount || request.Amount > company.MaxAmount)
                status = PaymentStatus.Rejected;
        }

        var payment = new Payment
        {
            WalletNumber = request.WalletNumber,
            Account      = user?.Name ?? request.Account,
            Email        = user?.Email ?? request.Email,
            Phone        = user?.Phone ?? request.Phone,
            Amount       = request.Amount,
            Currency     = request.Currency,
            Comment      = request.Comment,
            Status       = status,            
            UserId       = user?.Id,
            CompanyId    = company?.Id
        };

        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();
        // 👇 ВОТ ЭТО ДОБАВИТЬ
        await _db.Entry(payment)
            .Reference(p => p.Company)
            .LoadAsync();

        return MapToResponse(payment);
    }

    public async Task<List<PaymentResponse>> GetAllPaymentsAsync()
    {
        var payments = await _db.Payments
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return payments.Select(MapToResponse).ToList();
    }

    public async Task<List<PaymentResponse>> GetUserPaymentsAsync(
        Guid userId,
        string? status = null,
        string? currency = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null)
    {
        var query = _db.Payments
            .Include(p => p.Company)  
            .Where(p => p.UserId == userId);

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<PaymentStatus>(status, out var parsedStatus))
            query = query.Where(p => p.Status == parsedStatus);

        if (!string.IsNullOrEmpty(currency))
            query = query.Where(p => p.Currency == currency);

        if (dateFrom.HasValue)
            query = query.Where(p => p.CreatedAt >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(p => p.CreatedAt <= dateTo.Value.AddDays(1));

        var payments = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return payments.Select(MapToResponse).ToList();
    }

    // Private helper
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
            CreatedAt    = p.CreatedAt,
            CompanyName  = p.Company?.Name 
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