using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetProductHandler _handler;

    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetProductHandler(_productRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldReturnProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new GetProductCommand(productId);
        var product = new Product { Id = productId };
        var result = new GetProductResult { Id = productId };

        _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
        _mapper.Map<GetProductResult>(product).Returns(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(productId);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new GetProductCommand(productId);

        _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product?)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }
} 