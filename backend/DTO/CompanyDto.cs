public class CompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> AllowedCurrencies { get; set; } = new();
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
}