using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_productRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateProduct()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Title = "Test Product",
            Price = 99.99m,
            Description = "Test Description",
            Category = "Electronics"
        };

        var product = new Product { Id = Guid.NewGuid() };
        var result = new CreateProductResult { Id = product.Id };

        _mapper.Map<Product>(command).Returns(product);
        _productRepository.CreateAsync(product, Arg.Any<CancellationToken>()).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(product.Id);
    }
} 