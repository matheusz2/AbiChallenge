using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public UpdateProductRatingCommand Rating { get; set; } = new();
}

public class UpdateProductRatingCommand
{
    public double Rate { get; set; }
    public int Count { get; set; }
} 