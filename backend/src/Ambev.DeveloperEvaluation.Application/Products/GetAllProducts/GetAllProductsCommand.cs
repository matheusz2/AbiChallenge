using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public record GetAllProductsCommand : IRequest<GetAllProductsResult>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Order { get; }

    public GetAllProductsCommand(int page = 1, int pageSize = 10, string? order = null)
    {
        Page = page;
        PageSize = pageSize;
        Order = order;
    }
} 