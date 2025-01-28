using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system
/// </summary>
public class Sale : BaseEntity, ISale
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }    
    public string Customer { get; set; } = string.Empty ;
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<SaleItem>? Items { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    string ISale.Id => Id.ToString();
    string ISale.SaleNumber => SaleNumber;
    string ISale.Customer => Customer.ToString();
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }    
}