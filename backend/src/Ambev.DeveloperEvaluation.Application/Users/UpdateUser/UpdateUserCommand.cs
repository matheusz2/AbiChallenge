using MediatR;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Command for updating an existing user
/// </summary>
public class UpdateUserCommand : IRequest<UpdateUserResult>
{
    /// <summary>
    /// The unique identifier of the user to update
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's username
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// User's password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// User's first name
    /// </summary>
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// User's last name
    /// </summary>
    public string Lastname { get; set; } = string.Empty;

    /// <summary>
    /// User's city
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// User's street
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// User's street number
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// User's zip code
    /// </summary>
    public string Zipcode { get; set; } = string.Empty;

    /// <summary>
    /// User's latitude coordinate
    /// </summary>
    public string Lat { get; set; } = string.Empty;

    /// <summary>
    /// User's longitude coordinate
    /// </summary>
    public string Long { get; set; } = string.Empty;

    /// <summary>
    /// User's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// User's status
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// User's role
    /// </summary>
    public UserRole Role { get; set; }
} 