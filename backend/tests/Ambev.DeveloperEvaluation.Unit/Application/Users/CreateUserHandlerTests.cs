using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.Security;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

public class CreateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _handler = new CreateUserHandler(_userRepository, _mapper, _passwordHasher);
    }

    private CreateUserCommand GetValidCommand()
    {
        return new CreateUserCommand
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "Password123!",
            Firstname = "John",
            Lastname = "Doe",
            Phone = "(11) 99999-9999",
            City = "São Paulo",
            Street = "Rua Teste",
            Number = 123,
            Zipcode = "01234-567"
        };
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateUser()
    {
        // Arrange
        var command = GetValidCommand();
        var createdUser = new User { Id = Guid.NewGuid() };
        var result = new CreateUserResult { Id = createdUser.Id };

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User?)null);
        _userRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>()).Returns((User?)null);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(createdUser);
        _mapper.Map<CreateUserResult>(createdUser).Returns(result);
        _passwordHasher.HashPassword(command.Password).Returns("hashed");

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(createdUser.Id);
    }

    [Fact]
    public async Task Handle_EmailExists_ShouldThrowException()
    {
        // Arrange
        var command = GetValidCommand();
        command.Email = "existing@example.com";
        var existingUser = new User { Email = command.Email };
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(existingUser);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        // Arrange
        var command = new CreateUserCommand(); // todos os campos vazios

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ValidRequest_HashesPassword()
    {
        // Arrange
        var command = GetValidCommand();
        var createdUser = new User { Id = Guid.NewGuid(), Password = "hashed" };
        var result = new CreateUserResult { Id = createdUser.Id };

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User?)null);
        _userRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>()).Returns((User?)null);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(createdUser);
        _mapper.Map<CreateUserResult>(createdUser).Returns(result);
        _passwordHasher.HashPassword(command.Password).Returns("hashed");

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _passwordHasher.Received(1).HashPassword(command.Password);
        response.Should().NotBeNull();
        response.Id.Should().Be(createdUser.Id);
    }

    [Fact]
    public async Task Handle_ValidCommand_MapsCommandToUserEntity()
    {
        // Arrange
        var command = GetValidCommand();
        var createdUser = new User { Id = Guid.NewGuid() };
        var result = new CreateUserResult { Id = createdUser.Id };

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User?)null);
        _userRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>()).Returns((User?)null);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(createdUser);
        _mapper.Map<CreateUserResult>(createdUser).Returns(result);
        _passwordHasher.HashPassword(command.Password).Returns("hashed");

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapper.Received().Map<CreateUserResult>(createdUser);
        response.Should().NotBeNull();
        response.Id.Should().Be(createdUser.Id);
    }
} 