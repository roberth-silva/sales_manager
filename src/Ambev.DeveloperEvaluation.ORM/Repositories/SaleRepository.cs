using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new sale in the repository
    /// </summary>
    /// <param name="sale">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        sale.TotalAmount = CalculateTotalAmount(sale.Items);

        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Update an existence sale
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        try
        {
            

            var existingSale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

            if(existingSale == null)
            {
                throw new KeyNotFoundException($"Sale with Id {sale.Id} not found");
            }

            existingSale.Branch = sale.Branch;            
            existingSale.SaleNumber = sale.SaleNumber;
            existingSale.IsCancelled = sale.IsCancelled;
            existingSale.SaleDate = sale.SaleDate;
            existingSale.Customer = sale.Customer;
            existingSale.UpdatedAt = DateTime.UtcNow;
            existingSale.TotalAmount = CalculateTotalAmount(sale.Items);

            //update object except saleitems list
            //_context.Entry(existingSale).CurrentValues.SetValues(sale);


            foreach (var updatedSaleItem in sale.Items)
            {
                var existingItem = existingSale.Items.FirstOrDefault(i => i.Id == updatedSaleItem.Id);

                if (existingItem == null)                
                    existingSale.Items.Add(updatedSaleItem);
                else
                {
                    existingItem.IsCancelled = updatedSaleItem.IsCancelled;
                    existingItem.Discount = updatedSaleItem.Discount;
                    existingItem.UpdatedAt = DateTime.UtcNow;
                    existingItem.Product = updatedSaleItem.Product;
                    existingItem.Quantity = updatedSaleItem.Quantity;
                    existingItem.UnitPrice = updatedSaleItem.UnitPrice;
                    existingItem.TotalAmount = updatedSaleItem.TotalAmount;
                    //_context.Entry(existingItem).CurrentValues.SetValues(updatedSaleItem);
                }

            }

            //remove itens which doesn't belong to list anymore
            foreach (var existingItem in existingSale.Items.ToList())
            {
                if (!sale.Items.Any(i => i.Id == existingItem.Id))
                {
                    existingSale.Items.Remove(existingItem);
                    //_context.Remove(existingItem);
                    //_context.Entry(existingItem).State = EntityState.Deleted;
                }
            }            
            
            await _context.SaveChangesAsync(cancellationToken);
            return existingSale;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }        
        
    }

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.Include(p => p.Items).FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a sale by their salenumber
    /// </summary>
    /// <param name="salesNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<Sale?> GetBySalesNumberAsync(string salesNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.FirstOrDefaultAsync(o => o.SaleNumber == salesNumber, cancellationToken);
    }

    /// <summary>
    /// Retrieve the set of saleitem by their saleid value
    /// </summary>
    /// <param name="saleId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The saleitem set if found, null otherwise</returns>
    public async Task<IEnumerable<SaleItem>> GetSaleItemBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        return await _context.SalesItems.Where(o => o.SaleId == saleId).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Cancel a sale, turns isCacelled atribute to true or false
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isCancelled"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CancelAsync(Guid id, bool isCancelled, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        sale.IsCancelled = isCancelled;
        
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// CalculateTotalAmount
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    private decimal CalculateTotalAmount(List<SaleItem> items)
    {
        foreach (var item in items)
        {
            if (!item.IsCancelled)
            {
                item.Discount = GetDiscount(item.Quantity);
                item.TotalAmount = item.Quantity * item.UnitPrice * (1 - item.Discount);
            }            
        }
        return items.Sum(i => i.TotalAmount);
    }


    /// <summary>
    /// GetDiscount
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private decimal GetDiscount(int quantity)
    {
        if (quantity >= 10 && quantity <= 20) return 0.2m;
        if (quantity >= 4) return 0.1m;
        if (quantity > 20) throw new ArgumentException("Cannot sell more than 20 items per product.");
        return 0;
    }
}
