using StockMarketAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarketAPI.Services
{
    public class StockService
    {
        private readonly List<StockPrice> _prices;
        private readonly Random _random;

        public StockService()
        {
            _random = new Random();
            _prices = GenerateSampleData();
        }

        private List<StockPrice> GenerateSampleData()
        {
            var prices = new List<StockPrice>();
            var basePrice = 100.0m;
            var now = DateTime.UtcNow;

            // Örnek veri üretme
            for (int i = 0; i < 100; i++)
            {
                var price = basePrice + (decimal)_random.Next(-20, 21); // Fiyatları rastgele oluştur
                prices.Add(new StockPrice
                {
                    Date = now.AddMinutes(-i),  // Timestamp yerine Date kullanıldı
                    Price = price,
                    Symbol = "STOCK"  // Varsayılan hisse sembolü
                });
            }

            return prices;
        }

        // Fiyatları döndürme
        public IEnumerable<StockPrice> GetPrices()
        {
            return _prices;
        }

        // Ortalama fiyat hesaplama
        public decimal GetAveragePrice()
        {
            return _prices.Average(p => p.Price);
        }

        // En yüksek ve en düşük fiyatı döndürme
        public (decimal High, decimal Low) GetHighLowPrices()
        {
            return (_prices.Max(p => p.Price), _prices.Min(p => p.Price));
        }

        // Fiyat değişim yüzdesini hesaplama
        public decimal GetPriceChangePercentage()
        {
            var latestPrice = _prices.First().Price;
            var oldestPrice = _prices.Last().Price;
            return ((latestPrice - oldestPrice) / oldestPrice) * 100;
        }
    }
}
