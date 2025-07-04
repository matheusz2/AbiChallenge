using MediatR;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Command for retrieving all products with pagination, ordering and filtering
/// </summary>
public class GetAllProductsCommand : IRequest<GetAllProductsResult>
{
    /// <summary>
    /// Page number for pagination (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Optional ordering parameter (e.g., "title asc", "price desc")
    /// </summary>
    public string? Order { get; set; }

    /// <summary>
    /// Filter criteria for products
    /// </summary>
    public ProductFilter? Filter { get; set; }
} 