namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Response model for UpdateUser operation
/// </summary>
public class UpdateUserResponse
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
    public UpdateUserNameResponse Name { get; set; } = new();

    /// <summary>
    /// User's address information
    /// </summary>
    public UpdateUserAddressResponse Address { get; set; } = new();

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
/// User name information for response
/// </summary>
public class UpdateUserNameResponse
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
/// User address information for response
/// </summary>
public class UpdateUserAddressResponse
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
    public UpdateUserGeolocationResponse Geolocation { get; set; } = new();
}

/// <summary>
/// User geolocation information for response
/// </summary>
public class UpdateUserGeolocationResponse
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