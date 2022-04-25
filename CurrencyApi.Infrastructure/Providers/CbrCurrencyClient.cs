using System.Net.Http.Json;
using CurerencyApi.Infrastructure.Models;
using CurrencyApi.Core.Exceptions;
using CurrencyApi.Core.Interfaces;
using CurrencyApi.Core.Model;
using CurrencyApi.Core.Options;
using Microsoft.Extensions.Options;

namespace CurerencyApi.Infrastructure.Providers;

public class CbrCurrencyClient : ICurrencyClient, IDisposable
{
    private readonly HttpClient _client;
    
    public CbrCurrencyClient(string url, HttpClient? client = null)
    {
        _client = client ?? new HttpClient();
        _client.BaseAddress = new Uri(url);
    }

    public CbrCurrencyClient(IOptions<CurrencyClientOptions> options) : this(
        options.Value.Url)
    {
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesAsync(int offset, int take, CancellationToken? cancellationToken = null)
    {
        var response = await GetDailyCurrenciesAsync(cancellationToken);
        return response.Valute.Values.Skip(offset).Take(take).ToList();
    }

    public async Task<Currency> GetCurrencyByIdAsync(string id, CancellationToken? cancellationToken = null)
    {
        var response = await GetDailyCurrenciesAsync(cancellationToken);
        var result = response.Valute.Values.FirstOrDefault(currency => currency.ID == id);
        if (result is null) throw new CurrencyNotFoundException();
        return result;
    }

    public async Task<Currency> GetCurrencyByCharCodeAsync(string charCode, CancellationToken? cancellationToken = null)
    {
        var response = await GetDailyCurrenciesAsync(cancellationToken);
        return response.Valute[charCode];
    }

    private async Task<CbrResponseModel> GetDailyCurrenciesAsync(CancellationToken? cancellationToken)
    {
        var response = await _client.GetFromJsonAsync<CbrResponseModel>("daily_json.js", cancellationToken ?? default(CancellationToken));
        if (response is null) throw new CurrencyApiException("Api Request Responded with null object.");
        return response;
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}