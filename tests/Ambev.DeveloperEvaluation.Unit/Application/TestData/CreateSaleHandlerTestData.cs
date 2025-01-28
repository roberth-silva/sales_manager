using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - SaleNumber 
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<SaleItem> createSaleItemHandlerFaker = new Faker<SaleItem>()
        .RuleFor(si => si.Id, f => f.IndexFaker + 1) // Gera um ID sequencial
        .RuleFor(si => si.Product, f => f.Commerce.ProductName()) // Nome do produto
        .RuleFor(si => si.Quantity, f => f.Random.Int(1, 10)) // Quantidade entre 1 e 10
        .RuleFor(si => si.UnitPrice, f => f.Random.Decimal(10, 100)) // Preço unitário entre 10 e 100
        .RuleFor(si => si.Discount, f => f.Random.Decimal(0, 10)) // Desconto entre 0 e 10
        .RuleFor(si => si.TotalAmount, (f, si) => si.Quantity * si.UnitPrice - si.Discount) // Calcula o total
        .RuleFor(si => si.IsCancelled, f => f.Random.Bool(0.1f)); // 10% de chance de estar cancelado

    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(c => c.SaleNumber, f => f.Random.AlphaNumeric(10)) // Gera um número de venda alfanumérico
        .RuleFor(c => c.SaleDate, f => f.Date.Recent()) // Data recente
        .RuleFor(c => c.Customer, f => f.Person.FullName) // Nome completo de uma pessoa
        .RuleFor(c => c.TotalAmount, f => f.Random.Decimal(100, 10000)) // Valor total entre 100 e 10000
        .RuleFor(c => c.Branch, f => f.Address.City()) // Nome da cidade como filial
        .RuleFor(c => c.Items, f => createSaleItemHandlerFaker.Generate(f.Random.Int(1, 5)).ToList()) // Lista de 1 a 5 itens
        .RuleFor(c => c.IsCancelled, f => f.Random.Bool()); // Define se a venda foi cancelada



    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }
}
