using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Request model for getting all products with pagination, ordering and filtering
/// </summary>
public class GetAllProductsRequest
{
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

    // Basic Filters - exact match
    /// <summary>
    /// Filter by exact title
    /// </summary>
    public string? title { get; set; }

    /// <summary>
    /// Filter by exact category
    /// </summary>
    public string? category { get; set; }

    /// <summary>
    /// Filter by exact price
    /// </summary>
    public decimal? price { get; set; }

    /// <summary>
    /// Filter by exact description
    /// </summary>
    public string? description { get; set; }

    // Range Filters - numeric
    /// <summary>
    /// Minimum price filter
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be greater than or equal to 0")]
    public decimal? _minPrice { get; set; }

    /// <summary>
    /// Maximum price filter
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Maximum price must be greater than or equal to 0")]
    public decimal? _maxPrice { get; set; }

    /// <summary>
    /// Minimum rating filter
    /// </summary>
    [Range(0, 5, ErrorMessage = "Minimum rating must be between 0 and 5")]
    public double? _minRating { get; set; }

    /// <summary>
    /// Maximum rating filter
    /// </summary>
    [Range(0, 5, ErrorMessage = "Maximum rating must be between 0 and 5")]
    public double? _maxRating { get; set; }

    /// <summary>
    /// Minimum rating count filter
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Minimum rating count must be greater than or equal to 0")]
    public int? _minRatingCount { get; set; }

    /// <summary>
    /// Maximum rating count filter
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Maximum rating count must be greater than or equal to 0")]
    public int? _maxRatingCount { get; set; }

    // Properties for backward compatibility and mapping
    public int Page => _page;
    public int Size => _size;
    public string? Order => _order;
} 