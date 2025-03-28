using StockMarketAPI.Models;

namespace StockMarketAPI.Services
{
    public interface IStockAnalysisService
    {
        decimal CalculateAveragePrice(List<StockPrice> prices);
        (decimal high, decimal low) FindHighLow(List<StockPrice> prices);
        decimal CalculatePriceChange(List<StockPrice> prices);
    }
} 