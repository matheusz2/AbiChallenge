using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

/// <summary>
/// Request model for getting products by category with pagination and ordering
/// </summary>
public class GetProductsByCategoryRequest
{
    /// <summary>
    /// Category name to filter products
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Page number for pagination (default: 1)
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int _page { get; set; } = 1;

    /// <summary>
    /// Number of items per page (default: 10)
    /// </summary>
    [Range(1, 100, ErrorMessage = "Size must be between 1 and 100")]
    public int _size { get; set; } = 10;

    /// <summary>
    /// Ordering parameter (e.g., "title asc", "price desc")
    /// </summary>
    public string? _order { get; set; }

} 