using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.   
    /// </summary>
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(si => si.Id, f => f.IndexFaker + 1) // Generates sequential Id
        .RuleFor(si => si.Product, f => f.Commerce.ProductName()) // Product
        .RuleFor(si => si.Quantity, f => f.Random.Int(1, 10)) // Quantity between 1 and 10
        .RuleFor(si => si.UnitPrice, f => f.Random.Decimal(10, 100)) // Unit price between 10 and 100
        .RuleFor(si => si.Discount, f => f.Random.Decimal(0, 10)) // Descount between 0 and 10
        .RuleFor(si => si.TotalAmount, (f, si) => si.Quantity * si.UnitPrice - si.Discount) // amount
        .RuleFor(si => si.IsCancelled, f => f.Random.Bool(0.1f)); // 10% cancel chance

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(c => c.SaleNumber, f => f.Random.AlphaNumeric(10)) // sale number
        .RuleFor(c => c.SaleDate, f => f.Date.Recent()) // Recent date
        .RuleFor(c => c.Customer, f => f.Person.FullName) //Person full name
        .RuleFor(c => c.TotalAmount, f => f.Random.Decimal(100, 10000)) // Total amount bettween 100 and 10000
        .RuleFor(c => c.Branch, f => f.Address.City()) // City name
        .RuleFor(c => c.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 5)).ToList()) // List 1 to 5 items
        .RuleFor(c => c.IsCancelled, f => f.Random.Bool()); // Define if sale was cancel

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }
  
}
