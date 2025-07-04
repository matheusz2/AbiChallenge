using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale with product, quantity, and pricing information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale this item belongs to.
    /// Must be a valid GUID and cannot be empty.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// Must be a valid GUID and cannot be empty.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product.
    /// Must be greater than zero.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// Must be greater than zero.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the total price for this item (quantity * unit price).
    /// Calculated automatically.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the sale item's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property for the sale this item belongs to.
    /// </summary>
    public virtual Sale Sale { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the SaleItem class.
    /// </summary>
    public SaleItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the SaleItem class with specified values.
    /// </summary>
    /// <param name="productId">The product identifier</param>
    /// <param name="quantity">The quantity</param>
    /// <param name="unitPrice">The unit price</param>
    public SaleItem(Guid productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CreatedAt = DateTime.UtcNow;
        CalculateTotalPrice();
    }

    /// <summary>
    /// Performs validation of the sale item entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Product ID validity</list>
    /// <list type="bullet">Quantity greater than zero</list>
    /// <list type="bullet">Unit price greater than zero</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Calculates the total price for this item (quantity * unit price).
    /// </summary>
    public void CalculateTotalPrice()
    {
        TotalPrice = Math.Round(Quantity * UnitPrice, 2);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the quantity and recalculates the total price.
    /// </summary>
    /// <param name="newQuantity">The new quantity</param>
    /// <exception cref="ArgumentException">Thrown when quantity is less than or equal to zero</exception>
    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

        Quantity = newQuantity;
        CalculateTotalPrice();
    }

    /// <summary>
    /// Updates the unit price and recalculates the total price.
    /// </summary>
    /// <param name="newUnitPrice">The new unit price</param>
    /// <exception cref="ArgumentException">Thrown when unit price is less than or equal to zero</exception>
    public void UpdateUnitPrice(decimal newUnitPrice)
    {
        if (newUnitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

        UnitPrice = newUnitPrice;
        CalculateTotalPrice();
    }

    /// <summary>
    /// Gets the total price for this item.
    /// </summary>
    /// <returns>The calculated total price</returns>
    public decimal GetTotalPrice()
    {
        return TotalPrice;
    }

    /// <summary>
    /// Checks if this item has valid pricing information.
    /// </summary>
    /// <returns>True if quantity and unit price are valid, false otherwise</returns>
    public bool HasValidPricing()
    {
        return Quantity > 0 && UnitPrice > 0;
    }
} 