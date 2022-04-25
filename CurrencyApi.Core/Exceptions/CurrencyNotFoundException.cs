namespace CurrencyApi.Core.Exceptions;

public class CurrencyNotFoundException : CurrencyApiException
{
    public CurrencyNotFoundException() : base("Currency is not found")
    {
    }
}