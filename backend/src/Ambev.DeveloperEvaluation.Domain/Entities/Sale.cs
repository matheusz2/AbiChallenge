using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system with customer, branch, and item information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer associated with the sale.
    /// Must be a valid GUID and cannot be empty.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the sale occurred.
    /// Must be a valid GUID and cannot be empty.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// Must contain at least one item and cannot exceed 20 items.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the total amount of the sale before discount.
    /// Calculated automatically based on the sum of all items.
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Gets or sets the discount amount applied to the sale.
    /// Calculated automatically based on business rules.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage applied to the sale.
    /// Calculated automatically: 0% for <4 items, 10% for 4-9 items, 20% for 10-20 items.
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale after discount.
    /// Calculated as Subtotal - DiscountAmount.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the sale's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Customer ID validity</list>
    /// <list type="bullet">Branch ID validity</list>
    /// <list type="bullet">Items collection validity</list>
    /// <list type="bullet">Maximum items count (20)</list>
    /// <list type="bullet">Individual item validation</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Adds an item to the sale.
    /// Validates that the maximum number of items (20) is not exceeded.
    /// </summary>
    /// <param name="item">The sale item to add</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to add more than 20 items</exception>
    public void AddItem(SaleItem item)
    {
        if (Items.Count >= 20)
            throw new InvalidOperationException("Cannot add more than 20 items to a sale.");

        if (item == null)
            throw new ArgumentNullException(nameof(item));

        Items.Add(item);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes an item from the sale.
    /// </summary>
    /// <param name="item">The sale item to remove</param>
    /// <returns>True if the item was removed, false if it was not found</returns>
    public bool RemoveItem(SaleItem item)
    {
        if (item == null)
            return false;

        var removed = Items.Remove(item);
        if (removed)
            UpdatedAt = DateTime.UtcNow;

        return removed;
    }

    /// <summary>
    /// Clears all items from the sale.
    /// </summary>
    public void ClearItems()
    {
        Items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the number of items in the sale.
    /// </summary>
    /// <returns>The count of items</returns>
    public int GetItemsCount()
    {
        return Items.Count;
    }

    /// <summary>
    /// Checks if the sale has any items.
    /// </summary>
    /// <returns>True if the sale has items, false otherwise</returns>
    public bool HasItems()
    {
        return Items.Count > 0;
    }

    /// <summary>
    /// Checks if the sale can have more items added.
    /// </summary>
    /// <returns>True if the sale has less than 20 items, false otherwise</returns>
    public bool CanAddMoreItems()
    {
        return Items.Count < 20;
    }
}