using System.Security.Claims;
using backend.DTO;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/payments")]        // like @Controller('api/payments') in NestJS
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // POST /api/payments
    [Authorize] 
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _paymentService.CreatePaymentAsync(request,userId);
        return Ok(result);
    }

    // GET /api/payments
    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        var result = await _paymentService.GetAllPaymentsAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUserPayments()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _paymentService.GetUserPaymentsAsync(userId);
        return Ok(result);
    }

    // GET /api/payments/stats
    [Authorize]
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _paymentService.GetStatsAsync();
        return Ok(result);
    }
}