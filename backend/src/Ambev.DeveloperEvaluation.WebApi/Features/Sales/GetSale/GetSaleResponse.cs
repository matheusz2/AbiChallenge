using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleResponse
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the customer associated with the sale
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The unique identifier of the branch where the sale occurred
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The list of items included in the sale
    /// </summary>
    public List<GetSaleItemResponse> Items { get; set; } = new();

    /// <summary>
    /// The total amount of the sale before discount
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// The discount amount applied to the sale
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// The discount percentage applied to the sale
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// The total amount of the sale after discount
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// The date and time when the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the sale was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}