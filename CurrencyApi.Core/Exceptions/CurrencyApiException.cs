namespace CurrencyApi.Core.Exceptions;

public class CurrencyApiException : Exception
{
    public CurrencyApiException(string? message) : base(message)
    {
        
    }
}