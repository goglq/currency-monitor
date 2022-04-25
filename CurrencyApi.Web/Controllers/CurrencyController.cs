using CurrencyApi.Core.Exceptions;
using CurrencyApi.Core.Interfaces;
using CurrencyApi.Core.Model;
using CurrencyApi.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Web.Controllers;

[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    private readonly ILogger<CurrencyController> _logger;

    public CurrencyController(ICurrencyService currencyService, ILogger<CurrencyController> logger)
    {
        _currencyService = currencyService;
        _logger = logger;
    }
    
    [HttpGet("/currencies")]
    public async Task<ActionResult<ResponseModel<IEnumerable<Currency>>>> GetCurrencies([FromQuery] int offset = 0, [FromQuery] int take = 10)
    {
        try
        {
            var result = await _currencyService.GetCurrencies(offset, take);
            return new ResponseModel<IEnumerable<Currency>>(StatusCodes.Status200OK, true, result);
        }
        catch (CurrencyApiException e)
        {
            _logger.LogWarning(e, @"Warning Exception Type is {ExceptionType}", e.GetType().Name);
            return BadRequest(new ResponseModel<string>(StatusCodes.Status400BadRequest, false, "Bad Request"));
        }
        catch (Exception e)
        {
            _logger.LogError(e, @"Error Exception Type is {ExceptionType}", e.GetType().Name);
            return StatusCode(500);
        }
    }

    [HttpGet("/currency/{currencyId}")]
    public async Task<ActionResult<ResponseModel<Currency>>> GetCurrency([FromRoute] string currencyId, [FromQuery] bool cc = false)
    {
        try
        {
            var result = cc ? await _currencyService.GetCurrencyByCharCode(currencyId): await _currencyService.GetCurrencyById(currencyId);
            return new ResponseModel<Currency>(StatusCodes.Status200OK, true, result);
        }
        catch (CurrencyNotFoundException e)
        {
            _logger.LogWarning(e, @"Currency is not found. Exceptions type is {ExceptionType}", e.GetType().Name);
            var charCodeOrId = cc ? "char code" : "id";
            return NotFound(new ResponseModel<string>(StatusCodes.Status404NotFound, false,
                $"Couldn't find currency with {charCodeOrId}: {currencyId}"));
        }
        catch (CurrencyApiException e)
        {
            _logger.LogWarning(e, @"Warning Exception Type is {ExceptionType}", e.GetType().Name);
            return BadRequest(new ResponseModel<string>(StatusCodes.Status400BadRequest, false, "Bad Request"));
        }
        catch (Exception e)
        {
            _logger.LogError(e, @"Error Exception Type is {ExceptionType}", e.GetType().Name);
            return StatusCode(500);
        }
    }
}