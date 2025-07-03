using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

/// <summary>
/// Service interface for sale business logic operations
/// </summary>
public interface ISaleService
{
    /// <summary>
    /// Applies business rules to a sale, including discount calculations
    /// </summary>
    /// <param name="sale">The sale to apply business rules to</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task representing the asynchronous operation</returns>
    /// <remarks>
    /// Business rules applied:
    /// - Maximum 20 items per sale
    /// - Automatic discount calculation:
    ///   - 4+ items: 10% discount
    ///   - 10-20 items: 20% discount
    ///   - Less than 4 items: No discount
    /// - Calculation of subtotal, discount amount, and total
    /// </remarks>
    Task ApplyBusinessRulesAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Calculates the discount percentage based on the number of items
    /// </summary>
    /// <param name="itemsCount">The number of items in the sale</param>
    /// <returns>The discount percentage (0, 10, or 20)</returns>
    decimal CalculateDiscountPercentage(int itemsCount);

    /// <summary>
    /// Calculates the subtotal of all items in the sale
    /// </summary>
    /// <param name="sale">The sale to calculate subtotal for</param>
    /// <returns>The subtotal amount</returns>
    decimal CalculateSubtotal(Sale sale);

    /// <summary>
    /// Calculates the discount amount based on subtotal and discount percentage
    /// </summary>
    /// <param name="subtotal">The subtotal amount</param>
    /// <param name="discountPercentage">The discount percentage</param>
    /// <returns>The discount amount</returns>
    decimal CalculateDiscountAmount(decimal subtotal, decimal discountPercentage);

    /// <summary>
    /// Calculates the total amount after applying discount
    /// </summary>
    /// <param name="subtotal">The subtotal amount</param>
    /// <param name="discountAmount">The discount amount</param>
    /// <returns>The total amount</returns>
    decimal CalculateTotal(decimal subtotal, decimal discountAmount);

    /// <summary>
    /// Validates that the sale meets all business rules
    /// </summary>
    /// <param name="sale">The sale to validate</param>
    /// <returns>True if the sale is valid, false otherwise</returns>
    bool ValidateBusinessRules(Sale sale);
} 