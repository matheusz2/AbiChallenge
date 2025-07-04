using MediatR;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Command for creating a new user
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a user, 
/// including username, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateUserResult"/>.
/// </remarks>
public class CreateUserCommand : IRequest<CreateUserResult>
{
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