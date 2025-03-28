using StockMarketAPI.Models;
using StockMarketAPI.Services;
using Xunit;

namespace StockMarketAPI.Tests.Services;

public class StockAnalysisServiceTests
{
    private readonly StockAnalysisService _service;

    public StockAnalysisServiceTests()
    {
        _service = new StockAnalysisService();
    }

    [Fact]
    public void CalculateAveragePrice_ShouldReturnCorrectAverage()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m },
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 200m },
            new() { Date = DateTime.Now.AddMinutes(-10), Price = 300m }
        };

        // Act
        var average = _service.CalculateAveragePrice(prices);

        // Assert
        Assert.Equal(200m, average);
    }

    [Fact]
    public void FindHighLow_ShouldReturnCorrectValues()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m },
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 200m },
            new() { Date = DateTime.Now.AddMinutes(-10), Price = 300m }
        };

        // Act
        var (high, low) = _service.FindHighLow(prices);

        // Assert
        Assert.Equal(300m, high);
        Assert.Equal(100m, low);
    }

    [Fact]
    public void CalculatePriceChange_ShouldReturnCorrectPercentage()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now.AddMinutes(-10), Price = 100m }, // Oldest
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 150m },
            new() { Date = DateTime.Now, Price = 200m } // Newest
        };

        // Act
        var change = _service.CalculatePriceChange(prices);

        // Assert
        Assert.Equal(100m, change); // ((200 - 100) / 100) * 100 = 100%
    }

    [Fact]
    public void CalculateAveragePrice_ShouldReturnZeroForEmptyList()
    {
        // Arrange
        var prices = new List<StockPrice>();

        // Act
        var average = _service.CalculateAveragePrice(prices);

        // Assert
        Assert.Equal(0m, average);
    }

    [Fact]
    public void FindHighLow_ShouldReturnZeroForEmptyList()
    {
        // Arrange
        var prices = new List<StockPrice>();

        // Act
        var (high, low) = _service.FindHighLow(prices);

        // Assert
        Assert.Equal(0m, high);
        Assert.Equal(0m, low);
    }

    [Fact]
    public void CalculatePriceChange_ShouldReturnZeroForInsufficientData()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m }
        };

        // Act
        var change = _service.CalculatePriceChange(prices);

        // Assert
        Assert.Equal(0m, change);
    }
} 