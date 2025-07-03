using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Response model for GetAllSales operation with pagination
/// </summary>
public class GetAllSalesResult
{
    /// <summary>
    /// The list of sales for the current page
    /// </summary>
    public List<GetAllSalesItemResult> Items { get; set; } = new();

    /// <summary>
    /// The current page number
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// The number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The total number of sales
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// The total number of pages
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// Result model for individual sales in GetAllSales operation
/// </summary>
public class GetAllSalesItemResult
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
    /// The total amount of the sale after discount
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// The discount percentage applied to the sale
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// The number of items in the sale
    /// </summary>
    public int ItemsCount { get; set; }

    /// <summary>
    /// The date and time when the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The list of items included in the sale
    /// </summary>
    public List<Ambev.DeveloperEvaluation.Application.Sales.GetSale.GetSaleItemResult> Items { get; set; } = new();
} 