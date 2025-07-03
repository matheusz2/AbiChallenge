using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

public record GetProductsByCategoryCommand : IRequest<GetProductsByCategoryResult>
{
    public string Category { get; }
    public int Page { get; }
    public int PageSize { get; }
    public string? Order { get; }

    public GetProductsByCategoryCommand(string category, int page = 1, int pageSize = 10, string? order = null)
    {
        Category = category;
        Page = page;
        PageSize = pageSize;
        Order = order;
    }
} 