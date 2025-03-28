using StockMarketAPI.Models;
using StockMarketAPI.Services;
using Xunit;

namespace StockMarketAPI.Tests.Services;

public class StockPriceServiceTests
{
    private readonly StockPriceService _service;

    public StockPriceServiceTests()
    {
        _service = new StockPriceService();
    }

    [Fact]
    public void GenerateRandomPrices_ShouldReturnCorrectCount()
    {
        // Act
        var prices = _service.GenerateRandomPrices();

        // Assert
        Assert.Equal(10, prices.Count); // Default count is 10
    }

    [Fact]
    public void GenerateRandomPrices_ShouldHaveValidDates()
    {
        // Act
        var prices = _service.GenerateRandomPrices();

        // Assert
        Assert.True(prices[2].Date > prices[1].Date);
        Assert.True(prices[1].Date > prices[0].Date);
    }

    [Fact]
    public void GenerateRandomPrices_ShouldHaveValidPrices()
    {
        // Act
        var prices = _service.GenerateRandomPrices();

        // Assert
        foreach (var price in prices)
        {
            Assert.True(price.Price > 0);
            Assert.True(price.Price < 200); // Base price is 100, max variation is ±50
        }
    }
} 