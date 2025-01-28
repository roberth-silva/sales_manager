using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Represents a request to create a new Sale in the system.
/// </summary>
public class CancelSaleRequest
{
    public Guid Id { get; set; }   

    public bool IsCancelled { get; set; }
}