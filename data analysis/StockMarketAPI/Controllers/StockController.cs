using Microsoft.AspNetCore.Mvc;
using StockMarketAPI.Models;
using StockMarketAPI.Services;

namespace StockMarketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly StockService _stockService;

    public StockController(StockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet("prices")]
    public ActionResult<IEnumerable<StockPrice>> GetPrices()
    {
        return Ok(_stockService.GetPrices());
    }

    [HttpGet("analyze/average")]
    public ActionResult<decimal> GetAveragePrice()
    {
        return Ok(_stockService.GetAveragePrice());
    }

    [HttpGet("analyze/highlow")]
    public ActionResult<object> GetHighLowPrices()
    {
        var (high, low) = _stockService.GetHighLowPrices();
        return Ok(new { High = high, Low = low });
    }

    [HttpGet("analyze/change")]
    public ActionResult<decimal> GetPriceChangePercentage()
    {
        return Ok(_stockService.GetPriceChangePercentage());
    }
} 