using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Command for retrieving all sales with pagination
/// </summary>
public record GetAllSalesCommand : IRequest<GetAllSalesResult>
{
    /// <summary>
    /// The page number (default: 1)
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// The number of items per page (default: 10)
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Initializes a new instance of GetAllSalesCommand
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The number of items per page</param>
    public GetAllSalesCommand(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
} 