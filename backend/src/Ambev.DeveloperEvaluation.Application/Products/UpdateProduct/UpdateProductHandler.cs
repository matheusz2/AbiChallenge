using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingProduct == null)
            throw new InvalidOperationException($"Product with ID {command.Id} not found");

        existingProduct.Title = command.Title;
        existingProduct.Price = command.Price;
        existingProduct.Description = command.Description;
        existingProduct.Category = command.Category;
        existingProduct.Image = command.Image;
        existingProduct.RatingRate = command.Rating.Rate;
        existingProduct.RatingCount = command.Rating.Count;

        var updatedProduct = await _productRepository.UpdateAsync(existingProduct, cancellationToken);
        var result = _mapper.Map<UpdateProductResult>(updatedProduct);

        return result;
    }
} 