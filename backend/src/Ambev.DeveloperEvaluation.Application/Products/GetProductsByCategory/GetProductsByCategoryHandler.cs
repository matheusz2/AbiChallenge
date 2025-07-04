using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryCommand, GetProductsByCategoryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsByCategoryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryCommand command, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByCategoryAsync(command.Category, command.Page, command.PageSize, command.Order, cancellationToken);
        var allCategoryProducts = await _productRepository.GetByCategoryAsync(command.Category, 1, int.MaxValue, null, cancellationToken);
        var totalItems = allCategoryProducts.Count;
        var totalPages = (int)System.Math.Ceiling((double)totalItems / command.PageSize);

        return new GetProductsByCategoryResult
        {
            Data = _mapper.Map<List<GetProductsByCategoryItemResult>>(products),
            TotalItems = totalItems,
            CurrentPage = command.Page,
            TotalPages = totalPages
        };
    }
} 