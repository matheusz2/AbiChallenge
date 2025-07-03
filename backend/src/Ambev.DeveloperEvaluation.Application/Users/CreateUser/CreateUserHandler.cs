using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="passwordHasher">The password hasher service</param>
    public CreateUserHandler(
        IUserRepository userRepository,
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateUserValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Check if email already exists
        var existingUserByEmail = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUserByEmail != null)
            throw new InvalidOperationException($"User with email {command.Email} already exists");

        // Check if username already exists
        var existingUserByUsername = await _userRepository.GetByUsernameAsync(command.Username, cancellationToken);
        if (existingUserByUsername != null)
            throw new InvalidOperationException($"User with username {command.Username} already exists");

        var user = new User
        {
            Username = command.Username,
            Email = command.Email,
            Password = _passwordHasher.HashPassword(command.Password),
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role,
            Name = new Name
            {
                Firstname = command.Firstname,
                Lastname = command.Lastname
            },
            Address = new Address
            {
                City = command.City,
                Street = command.Street,
                Number = command.Number,
                Zipcode = command.Zipcode,
                Geolocation = new Geolocation
                {
                    Lat = command.Lat,
                    Long = command.Long
                }
            }
        };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
        return _mapper.Map<CreateUserResult>(createdUser);
    }
}
