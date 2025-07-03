using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Handler for processing GetAllSalesCommand requests
/// </summary>
public class GetAllSalesHandler : IRequestHandler<GetAllSalesCommand, GetAllSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllSalesHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetAllSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllSalesCommand request
    /// </summary>
    /// <param name="command">The GetAllSales command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The paginated sales list</returns>
    public async Task<GetAllSalesResult> Handle(GetAllSalesCommand command, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync(command.Page, command.PageSize, cancellationToken);
        var totalCount = await _saleRepository.GetTotalCountAsync(cancellationToken);
        
        var totalPages = (int)Math.Ceiling((double)totalCount / command.PageSize);
        
        var result = new GetAllSalesResult
        {
            Items = _mapper.Map<List<GetAllSalesItemResult>>(sales),
            Page = command.Page,
            PageSize = command.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        
        return result;
    }
} 