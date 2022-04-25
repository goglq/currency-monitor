using CurrencyApi.Core.Model;

namespace CurrencyApi.Core.Interfaces;

public interface ICurrencyClient
{
    Task<IEnumerable<Currency>> GetCurrenciesAsync(int offset, int take, CancellationToken? cancellationToken = null);

    Task<Currency> GetCurrencyByIdAsync(string id, CancellationToken? cancellationToken = null);
    
    Task<Currency> GetCurrencyByCharCodeAsync(string charCode, CancellationToken? cancellationToken = null);
}