using backend.Data;
using backend.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class CompanyService
{
    private readonly AppDbContext _db;

    public CompanyService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CompanyResponse>> GetAllCompaniesAsync(
        string? search = null,
        string? currency = null)
    {
        var query = _db.Companies.Where(c => c.IsActive);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(c => c.Name.ToLower().Contains(search.ToLower()) ||
                                    c.Description.ToLower().Contains(search.ToLower()));

        if (!string.IsNullOrEmpty(currency))
            query = query.Where(c => c.AllowedCurrencies.Contains(currency));

        var companies = await query.ToListAsync();

        return companies.Select(c => new CompanyResponse
        {
            Id                = c.Id,
            Name              = c.Name,
            Description       = c.Description,
            AllowedCurrencies = c.AllowedCurrencies.Split(',').ToList(),
            MinAmount         = c.MinAmount,
            MaxAmount         = c.MaxAmount
        }).ToList();
    }
}