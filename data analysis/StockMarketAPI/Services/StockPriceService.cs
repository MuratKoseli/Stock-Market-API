using StockMarketAPI.Models;

namespace StockMarketAPI.Services;

public class StockPriceService : IStockPriceService
{
    private readonly Random _random = new Random();
    private const int DefaultCount = 10;

    public List<StockPrice> GenerateRandomPrices()
    {
        var prices = new List<StockPrice>();
        var basePrice = 100m;
        var currentDate = DateTime.Now;

        for (int i = 0; i < DefaultCount; i++)
        {
            var price = new StockPrice
            {
                Date = currentDate.AddMinutes(-i * 5), // Her 5 dakikada bir veri
                Price = basePrice + (decimal)(_random.NextDouble() * 20 - 10) // -10 ile +10 arasında rastgele değişim
            };
            prices.Add(price);
        }

        return prices.OrderBy(p => p.Date).ToList();
    }
} 