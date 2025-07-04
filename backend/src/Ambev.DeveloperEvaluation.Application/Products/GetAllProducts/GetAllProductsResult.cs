using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsResult
{
    public List<GetAllProductsItemResult> Data { get; set; } = new();
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}

public class GetAllProductsItemResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ProductRatingResult Rating { get; set; } = new();
}

public class ProductRatingResult
{
    public double Rate { get; set; }
    public int Count { get; set; }
} 