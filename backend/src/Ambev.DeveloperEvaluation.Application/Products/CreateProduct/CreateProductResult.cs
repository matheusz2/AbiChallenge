using System;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public CreateProductRatingResult Rating { get; set; } = new();
}

public class CreateProductRatingResult
{
    public double Rate { get; set; }
    public int Count { get; set; }
} 