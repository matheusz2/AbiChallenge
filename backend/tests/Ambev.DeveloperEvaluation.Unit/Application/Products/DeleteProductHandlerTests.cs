using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class DeleteProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new DeleteProductHandler(_productRepository);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldDeleteProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new DeleteProductCommand(productId);
        var product = new Product { Id = productId };

        _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
        _productRepository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldReturnFalse()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new DeleteProductCommand(productId);

        _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product?)null);
        _productRepository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().BeFalse();
    }
} 