using Microsoft.AspNetCore.Mvc;
using Moq;
using StockMarketAPI.Controllers;
using StockMarketAPI.Models;
using StockMarketAPI.Services;
using Xunit;

namespace StockMarketAPI.Tests.Controllers;

public class BorsaControllerTests
{
    private readonly Mock<IStockPriceService> _mockStockPriceService;
    private readonly Mock<IStockAnalysisService> _mockStockAnalysisService;
    private readonly BorsaController _controller;

    public BorsaControllerTests()
    {
        _mockStockPriceService = new Mock<IStockPriceService>();
        _mockStockAnalysisService = new Mock<IStockAnalysisService>();
        _controller = new BorsaController(_mockStockPriceService.Object, _mockStockAnalysisService.Object);
    }

    [Fact]
    public void GetPrices_ShouldReturnOkResult()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m },
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 200m }
        };
        _mockStockPriceService.Setup(x => x.GenerateRandomPrices())
            .Returns(prices);

        // Act
        var result = _controller.GetPrices();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPrices = Assert.IsType<List<StockPrice>>(okResult.Value);
        Assert.Equal(2, returnedPrices.Count);
    }

    [Fact]
    public void GetAnalysis_ShouldReturnOkResult()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m },
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 200m }
        };
        _mockStockPriceService.Setup(x => x.GenerateRandomPrices())
            .Returns(prices);
        _mockStockAnalysisService.Setup(x => x.CalculateAveragePrice(It.IsAny<List<StockPrice>>()))
            .Returns(150m);
        _mockStockAnalysisService.Setup(x => x.FindHighLow(It.IsAny<List<StockPrice>>()))
            .Returns((200m, 100m));
        _mockStockAnalysisService.Setup(x => x.CalculatePriceChange(It.IsAny<List<StockPrice>>()))
            .Returns(50m);

        // Act
        var result = _controller.GetAnalysis();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void GetAveragePrice_ShouldReturnOkResult()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m },
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 200m }
        };
        _mockStockPriceService.Setup(x => x.GenerateRandomPrices())
            .Returns(prices);
        _mockStockAnalysisService.Setup(x => x.CalculateAveragePrice(It.IsAny<List<StockPrice>>()))
            .Returns(150m);

        // Act
        var result = _controller.GetAveragePrice();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var averagePrice = Assert.IsType<decimal>(okResult.Value);
        Assert.Equal(150m, averagePrice);
    }

    [Fact]
    public void GetHighLow_ShouldReturnOkResult()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m },
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 200m }
        };
        _mockStockPriceService.Setup(x => x.GenerateRandomPrices())
            .Returns(prices);
        _mockStockAnalysisService.Setup(x => x.FindHighLow(It.IsAny<List<StockPrice>>()))
            .Returns((200m, 100m));

        // Act
        var result = _controller.GetHighLow();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void GetPriceChange_ShouldReturnOkResult()
    {
        // Arrange
        var prices = new List<StockPrice>
        {
            new() { Date = DateTime.Now, Price = 100m },
            new() { Date = DateTime.Now.AddMinutes(-5), Price = 200m }
        };
        _mockStockPriceService.Setup(x => x.GenerateRandomPrices())
            .Returns(prices);
        _mockStockAnalysisService.Setup(x => x.CalculatePriceChange(It.IsAny<List<StockPrice>>()))
            .Returns(50m);

        // Act
        var result = _controller.GetPriceChange();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var priceChange = Assert.IsType<decimal>(okResult.Value);
        Assert.Equal(50m, priceChange);
    }
} 