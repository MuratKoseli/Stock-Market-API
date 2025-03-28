using StockMarketAPI.Models;

namespace StockMarketAPI.Services;

public class StockAnalysisService : IStockAnalysisService
{
    public decimal CalculateAveragePrice(List<StockPrice> prices)
    {
        if (prices == null || !prices.Any())
            return 0;

        return prices.Average(p => p.Price);
    }

    public (decimal high, decimal low) FindHighLow(List<StockPrice> prices)
    {
        if (prices == null || !prices.Any())
            return (0, 0);

        var high = prices.Max(p => p.Price);
        var low = prices.Min(p => p.Price);

        return (high, low);
    }

    public decimal CalculatePriceChange(List<StockPrice> prices)
    {
        if (prices == null || prices.Count < 2)
            return 0;

        var firstPrice = prices.First().Price;
        var lastPrice = prices.Last().Price;

        return ((lastPrice - firstPrice) / firstPrice) * 100;
    }
} 