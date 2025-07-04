namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

/// <summary>
/// Request model for retrieving all carts with pagination
/// </summary>
public class GetAllCartsRequest
{
    /// <summary>
    /// Page number (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Ordering parameter
    /// </summary>
    public string? OrderBy { get; set; }
} 