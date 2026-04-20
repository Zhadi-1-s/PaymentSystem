using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompaniesController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    // GET /api/companies — public, no auth needed
    [HttpGet]
    public async Task<IActionResult> GetCompanies(
        [FromQuery] string? search = null,
        [FromQuery] string? currency = null)
    {
        var result = await _companyService.GetAllCompaniesAsync(search, currency);
        return Ok(result);
    }
}