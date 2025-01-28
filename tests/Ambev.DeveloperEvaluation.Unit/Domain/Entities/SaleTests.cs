using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that when a isCanceled atrivute is chaged to false.
    /// </summary>
    [Fact(DisplayName = "Sale isCanceled atribute should change to false")]
    public void Given_SuspendedSale_When_Activated_Then_StatusShouldBeActive()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();


        // Act
        sale.IsCancelled = false;

        // Assert
        Assert.Equal(false, sale.IsCancelled);
    }    
}
