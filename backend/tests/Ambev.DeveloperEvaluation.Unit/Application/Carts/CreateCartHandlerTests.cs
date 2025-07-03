using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCartHandler(_cartRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateCart()
    {
        // Arrange
        var command = new CreateCartCommand
        {
            UserId = 1,
            Date = DateTime.Now,
            Products = new List<CreateCartProductCommand>
            {
                new CreateCartProductCommand { ProductId = 1, Quantity = 2 }
            }
        };

        var cart = new Cart { Id = Guid.NewGuid() };
        var result = new CreateCartResult { Id = cart.Id };

        _mapper.Map<Cart>(command).Returns(cart);
        _cartRepository.CreateAsync(cart, Arg.Any<CancellationToken>()).Returns(cart);
        _mapper.Map<CreateCartResult>(cart).Returns(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(cart.Id);
    }
} 