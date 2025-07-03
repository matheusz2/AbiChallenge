using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Handler for processing UpdateUserCommand requests
/// </summary>
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of UpdateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="passwordHasher">The password hasher service</param>
    public UpdateUserHandler(
        IUserRepository userRepository, 
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Handles the UpdateUserCommand request
    /// </summary>
    /// <param name="command">The UpdateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user details</returns>
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _userRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingUser == null)
            throw new InvalidOperationException($"User with ID {command.Id} not found");

        // Check if email is being changed to one that already exists (excluding current user)
        if (existingUser.Email != command.Email)
        {
            var existingUserByEmail = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingUserByEmail != null && existingUserByEmail.Id != command.Id)
                throw new InvalidOperationException($"User with email {command.Email} already exists");
        }

        // Check if username is being changed to one that already exists (excluding current user)
        if (existingUser.Username != command.Username)
        {
            var existingUserByUsername = await _userRepository.GetByUsernameAsync(command.Username, cancellationToken);
            if (existingUserByUsername != null && existingUserByUsername.Id != command.Id)
                throw new InvalidOperationException($"User with username {command.Username} already exists");
        }

        // Update user properties
        existingUser.Username = command.Username;
        existingUser.Email = command.Email;
        existingUser.Phone = command.Phone;
        existingUser.Role = command.Role;
        existingUser.Status = command.Status;
        existingUser.Name.Firstname = command.Firstname;
        existingUser.Name.Lastname = command.Lastname;
        existingUser.Address.City = command.City;
        existingUser.Address.Street = command.Street;
        existingUser.Address.Number = command.Number;
        existingUser.Address.Zipcode = command.Zipcode;
        existingUser.Address.Geolocation.Lat = command.Lat;
        existingUser.Address.Geolocation.Long = command.Long;
        existingUser.UpdatedAt = DateTime.UtcNow;

        // Update password if provided
        if (!string.IsNullOrWhiteSpace(command.Password))
        {
            existingUser.Password = _passwordHasher.HashPassword(command.Password);
        }

        var updatedUser = await _userRepository.UpdateAsync(existingUser, cancellationToken);
        return _mapper.Map<UpdateUserResult>(updatedUser);
    }
} 