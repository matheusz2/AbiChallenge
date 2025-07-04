using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Response model for GetAllUsers operation
/// </summary>
public class GetAllUsersResult
{
    /// <summary>
    /// The list of users for the current page
    /// </summary>
    public List<GetAllUsersItemResult> Data { get; set; } = [];

    /// <summary>
    /// Total number of users
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// Individual user item in the GetAllUsers response
/// </summary>
public class GetAllUsersItemResult
{
    /// <summary>
    /// User's unique identifier
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
    /// User's password (hashed)
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// User's name information
    /// </summary>
    public UserNameResult Name { get; set; } = new();

    /// <summary>
    /// User's address information
    /// </summary>
    public UserAddressResult Address { get; set; } = new();

    /// <summary>
    /// User's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// User's status
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// User's role
    /// </summary>
    public string Role { get; set; } = string.Empty;
}

/// <summary>
/// User name information
/// </summary>
public class UserNameResult
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
/// User address information
/// </summary>
public class UserAddressResult
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
    public UserGeolocationResult Geolocation { get; set; } = new();
}

/// <summary>
/// User geolocation information
/// </summary>
public class UserGeolocationResult
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