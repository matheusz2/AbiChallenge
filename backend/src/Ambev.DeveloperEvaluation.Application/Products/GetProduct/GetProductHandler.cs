using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductResult> Handle(GetProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {command.Id} not found");

        return _mapper.Map<GetProductResult>(product);
    }
} 