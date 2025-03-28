using Microsoft.AspNetCore.Mvc;
using StockMarketAPI.Models;
using StockMarketAPI.Services;

namespace StockMarketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorsaController : ControllerBase
{
    private readonly IStockPriceService _stockPriceService;
    private readonly IStockAnalysisService _stockAnalysisService;

    public BorsaController(IStockPriceService stockPriceService, IStockAnalysisService stockAnalysisService)
    {
        _stockPriceService = stockPriceService;
        _stockAnalysisService = stockAnalysisService;
    }

    [HttpGet("prices")]
    public ActionResult<List<StockPrice>> GetPrices()
    {
        var prices = _stockPriceService.GenerateRandomPrices();
        return Ok(prices);
    }

    [HttpGet("analysis")]
    public ActionResult<object> GetAnalysis()
    {
        var prices = _stockPriceService.GenerateRandomPrices();
        var averagePrice = _stockAnalysisService.CalculateAveragePrice(prices);
        var (high, low) = _stockAnalysisService.FindHighLow(prices);
        var priceChange = _stockAnalysisService.CalculatePriceChange(prices);

        return Ok(new
        {
            AveragePrice = averagePrice,
            HighPrice = high,
            LowPrice = low,
            PriceChange = priceChange
        });
    }

    [HttpGet("average")]
    public ActionResult<decimal> GetAveragePrice()
    {
        var prices = _stockPriceService.GenerateRandomPrices();
        var averagePrice = _stockAnalysisService.CalculateAveragePrice(prices);
        return Ok(averagePrice);
    }

    [HttpGet("highlow")]
    public ActionResult<object> GetHighLow()
    {
        var prices = _stockPriceService.GenerateRandomPrices();
        var (high, low) = _stockAnalysisService.FindHighLow(prices);

        return Ok(new
        {
            HighPrice = high,
            LowPrice = low
        });
    }

    [HttpGet("pricechange")]
    public ActionResult<decimal> GetPriceChange()
    {
        var prices = _stockPriceService.GenerateRandomPrices();
        var priceChange = _stockAnalysisService.CalculatePriceChange(prices);
        return Ok(priceChange);
    }
} 