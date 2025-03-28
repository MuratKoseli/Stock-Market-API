using StockMarketAPI.Models;

namespace StockMarketAPI.Services;

public interface IStockPriceService
{
    List<StockPrice> GenerateRandomPrices();
} 