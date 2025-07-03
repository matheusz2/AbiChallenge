namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryRequest
{
    public string Category { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
} 