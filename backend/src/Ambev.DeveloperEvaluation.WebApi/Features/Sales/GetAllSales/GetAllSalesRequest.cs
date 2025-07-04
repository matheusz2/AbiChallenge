namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Request model for getting all sales with pagination
/// </summary>
public class GetAllSalesRequest
{
    /// <summary>
    /// The page number (default: 1)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// The number of items per page (default: 10)
    /// </summary>
    public int PageSize { get; set; } = 10;
} 