using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public CreateProductRatingCommand Rating { get; set; } = new();
}

public class CreateProductRatingCommand
{
    public double Rate { get; set; }
    public int Count { get; set; }
} 