namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Represents the response returned after successfully cancel a sale
/// </summary>
public class CancelSaleResult
{
    public Guid Id { get; set; }
    public bool IsCancelled { get; set; }
}
