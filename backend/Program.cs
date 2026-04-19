using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // 👈 replaces AddOpenApi

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=payments.db"));

builder.Services.AddScoped<PaymentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  // 👈 this gives you the visual UI
}

app.MapControllers();
app.Run();