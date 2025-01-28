using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Creates a new sale in the repository
    /// </summary>
    /// <param name="sale">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existence sale
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by their salenumber
    /// </summary>
    /// <param name="salesNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetBySalesNumberAsync(string salesNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve the set of saleitem by their saleid value
    /// </summary>
    /// <param name="saleId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The saleitem set if found, null otherwise</returns>
    Task<IEnumerable<SaleItem>> GetSaleItemBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancel a sale, turns isCacelled atribute to true or false
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isCancelled"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> CancelAsync(Guid id, bool isCancelled, CancellationToken cancellationToken = default);
}
