using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class GetCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly GetCartHandler _handler;

    public GetCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetCartHandler(_cartRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldReturnCart()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var command = new GetCartCommand(cartId);
        var cart = new Cart { Id = cartId };
        var result = new GetCartResult { Id = cartId };

        _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
        _mapper.Map<GetCartResult>(cart).Returns(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(cartId);
    }

    [Fact]
    public async Task Handle_CartNotFound_ShouldThrowException()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var command = new GetCartCommand(cartId);

        _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }
} 