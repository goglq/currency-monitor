using CurrencyApi.Core.Model;

namespace CurrencyApi.Core.Interfaces;

public interface ICurrencyService
{
    public Task<IEnumerable<Currency>> GetCurrencies(int offset, int take);

    public Task<Currency> GetCurrencyById(string id);
    
    public Task<Currency> GetCurrencyByCharCode(string charCode);
}