namespace Ambev.DeveloperEvaluation.Domain.Models;

/// <summary>
/// Filter criteria for users
/// </summary>
public class UserFilter
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? Status { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Phone { get; set; }

    /// <summary>
    /// Checks if the username contains wildcard characters
    /// </summary>
    public bool HasUsernameWildcard => !string.IsNullOrEmpty(Username) && (Username.StartsWith("*") || Username.EndsWith("*"));

    /// <summary>
    /// Checks if the email contains wildcard characters
    /// </summary>
    public bool HasEmailWildcard => !string.IsNullOrEmpty(Email) && (Email.StartsWith("*") || Email.EndsWith("*"));

    /// <summary>
    /// Checks if the first name contains wildcard characters
    /// </summary>
    public bool HasFirstNameWildcard => !string.IsNullOrEmpty(FirstName) && (FirstName.StartsWith("*") || FirstName.EndsWith("*"));

    /// <summary>
    /// Checks if the last name contains wildcard characters
    /// </summary>
    public bool HasLastNameWildcard => !string.IsNullOrEmpty(LastName) && (LastName.StartsWith("*") || LastName.EndsWith("*"));

    /// <summary>
    /// Checks if the city contains wildcard characters
    /// </summary>
    public bool HasCityWildcard => !string.IsNullOrEmpty(City) && (City.StartsWith("*") || City.EndsWith("*"));

    /// <summary>
    /// Checks if the street contains wildcard characters
    /// </summary>
    public bool HasStreetWildcard => !string.IsNullOrEmpty(Street) && (Street.StartsWith("*") || Street.EndsWith("*"));

    /// <summary>
    /// Checks if the phone contains wildcard characters
    /// </summary>
    public bool HasPhoneWildcard => !string.IsNullOrEmpty(Phone) && (Phone.StartsWith("*") || Phone.EndsWith("*"));

    /// <summary>
    /// Gets the username without wildcard characters
    /// </summary>
    public string GetUsernameWithoutWildcard()
    {
        if (string.IsNullOrEmpty(Username)) return string.Empty;
        return Username.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the email without wildcard characters
    /// </summary>
    public string GetEmailWithoutWildcard()
    {
        if (string.IsNullOrEmpty(Email)) return string.Empty;
        return Email.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the first name without wildcard characters
    /// </summary>
    public string GetFirstNameWithoutWildcard()
    {
        if (string.IsNullOrEmpty(FirstName)) return string.Empty;
        return FirstName.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the last name without wildcard characters
    /// </summary>
    public string GetLastNameWithoutWildcard()
    {
        if (string.IsNullOrEmpty(LastName)) return string.Empty;
        return LastName.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the city without wildcard characters
    /// </summary>
    public string GetCityWithoutWildcard()
    {
        if (string.IsNullOrEmpty(City)) return string.Empty;
        return City.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the street without wildcard characters
    /// </summary>
    public string GetStreetWithoutWildcard()
    {
        if (string.IsNullOrEmpty(Street)) return string.Empty;
        return Street.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the phone without wildcard characters
    /// </summary>
    public string GetPhoneWithoutWildcard()
    {
        if (string.IsNullOrEmpty(Phone)) return string.Empty;
        return Phone.Replace("*", "").Trim();
    }
} 