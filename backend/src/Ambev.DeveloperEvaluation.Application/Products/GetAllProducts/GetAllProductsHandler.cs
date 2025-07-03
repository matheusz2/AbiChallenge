using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, GetAllProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetAllProductsResult> Handle(GetAllProductsCommand command, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(command.Page, command.PageSize, command.Order, cancellationToken);
        var totalItems = await GetTotalCountAsync(cancellationToken);
        var totalPages = (int)System.Math.Ceiling((double)totalItems / command.PageSize);

        return new GetAllProductsResult
        {
            Data = _mapper.Map<List<GetAllProductsItemResult>>(products),
            TotalItems = totalItems,
            CurrentPage = command.Page,
            TotalPages = totalPages
        };
    }

    private async Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        var allProducts = await _productRepository.GetAllAsync(1, int.MaxValue, null, cancellationToken);
        return allProducts.Count;
    }
} 