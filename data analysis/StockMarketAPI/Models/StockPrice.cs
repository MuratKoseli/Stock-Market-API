namespace StockMarketAPI.Models
{
    public class StockPrice
    {
        public DateTime Date { get; set; }  // Fiyatın alındığı tarih
        public decimal Price { get; set; }  // Fiyat
        public string Symbol { get; set; } = "STOCK";  // Hisse senedi sembolü, varsayılan 'STOCK'
    }
}