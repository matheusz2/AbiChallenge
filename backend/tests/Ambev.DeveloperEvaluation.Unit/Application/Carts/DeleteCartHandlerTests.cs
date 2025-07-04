using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class DeleteCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _handler = new DeleteCartHandler(_cartRepository);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldDeleteCart()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var command = new DeleteCartCommand(cartId);
        var cart = new Cart { Id = cartId };

        _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
        _cartRepository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_CartNotFound_ShouldReturnFalse()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var command = new DeleteCartCommand(cartId);

        _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart?)null);
        _cartRepository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Success.Should().BeFalse();
    }
} 