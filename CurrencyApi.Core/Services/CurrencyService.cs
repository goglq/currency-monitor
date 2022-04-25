using CurrencyApi.Core.Interfaces;
using CurrencyApi.Core.Model;
using Microsoft.Extensions.Logging;

namespace CurrencyApi.Core.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyClient _client;

    public CurrencyService(ICurrencyClient client)
    {
        _client = client;
    }
    
    public Task<IEnumerable<Currency>> GetCurrencies(int offset, int take)
    {
        return _client.GetCurrenciesAsync(offset, take);
    }

    public Task<Currency> GetCurrencyById(string id)
    {
        return _client.GetCurrencyByIdAsync(id);
    }

    public Task<Currency> GetCurrencyByCharCode(string charCode)
    {
        return _client.GetCurrencyByCharCodeAsync(charCode);
    }
}