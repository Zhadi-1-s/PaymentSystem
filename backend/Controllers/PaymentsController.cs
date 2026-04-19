using backend.DTO;
using backend.Services;
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
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
    {
        var result = await _paymentService.CreatePaymentAsync(request);
        return Ok(result);
    }

    // GET /api/payments
    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        var result = await _paymentService.GetAllPaymentsAsync();
        return Ok(result);
    }

    // GET /api/payments/stats
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _paymentService.GetStatsAsync();
        return Ok(result);
    }
}