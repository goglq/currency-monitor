namespace CurrencyApi.Core.Model;

public class Currency
{
    public string ID { get; set; }
    
    public string NumCode { get; set; }
    
    public string CharCode { get; set; }
    
    public decimal Nominal { get; set; }
    
    public string Name { get; set; }
    
    public decimal Value { get; set; }
    
    public decimal Previous { get; set; }
}