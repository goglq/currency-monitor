using CurrencyApi.Core.Model;

namespace CurerencyApi.Infrastructure.Models;

public class CbrResponseModel
{
    public DateTime Date { get; set; }
    
    public DateTime PreviousDate { get; set; }

    public string PreviousUrl { get; set; } = "";
    
    public DateTime Timestamp { get; set; }

    public Dictionary<string, Currency> Valute { get; set; } = new();
}