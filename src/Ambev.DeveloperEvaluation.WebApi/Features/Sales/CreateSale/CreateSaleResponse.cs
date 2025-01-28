using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The current status of the user
    /// </summary>
    public string Branch { get; set; } = string.Empty;
    public List<SaleItem>? Items { get; set; }

    public bool IsCancelled { get; set; }
}
