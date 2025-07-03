using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Request model for creating a user
/// </summary>
public class CreateUserRequest
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
    /// User's name information
    /// </summary>
    public CreateUserNameRequest Name { get; set; } = new();

    /// <summary>
    /// User's address information
    /// </summary>
    public CreateUserAddressRequest Address { get; set; } = new();

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

/// <summary>
/// User name information for create request
/// </summary>
public class CreateUserNameRequest
{
    /// <summary>
    /// User's first name
    /// </summary>
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// User's last name
    /// </summary>
    public string Lastname { get; set; } = string.Empty;
}

/// <summary>
/// User address information for create request
/// </summary>
public class CreateUserAddressRequest
{
    /// <summary>
    /// City name
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Street name
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Street number
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// ZIP code
    /// </summary>
    public string Zipcode { get; set; } = string.Empty;

    /// <summary>
    /// Geolocation coordinates
    /// </summary>
    public CreateUserGeolocationRequest Geolocation { get; set; } = new();
}

/// <summary>
/// User geolocation information for create request
/// </summary>
public class CreateUserGeolocationRequest
{
    /// <summary>
    /// Latitude coordinate
    /// </summary>
    public string Lat { get; set; } = string.Empty;

    /// <summary>
    /// Longitude coordinate
    /// </summary>
    public string Long { get; set; } = string.Empty;
}