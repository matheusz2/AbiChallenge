using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Handler for processing GetAllCartsCommand requests
/// </summary>
public class GetAllCartsHandler : IRequestHandler<GetAllCartsCommand, GetAllCartsResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllCartsHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetAllCartsHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllCartsCommand request
    /// </summary>
    /// <param name="request">The GetAllCarts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The paginated list of carts</returns>
    public async Task<GetAllCartsResult> Handle(GetAllCartsCommand request, CancellationToken cancellationToken)
    {
        var carts = await _cartRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, cancellationToken);
        var totalCount = await _cartRepository.GetTotalCountAsync(cancellationToken);

        var result = new GetAllCartsResult
        {
            Data = _mapper.Map<List<GetAllCartsItemResult>>(carts),
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };

        return result;
    }
} 