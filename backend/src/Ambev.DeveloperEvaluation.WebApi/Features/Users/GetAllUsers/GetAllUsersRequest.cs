using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;

/// <summary>
/// Request model for getting all users with pagination, ordering and filtering
/// </summary>
public class GetAllUsersRequest
{
    /// <summary>
    /// Page number for pagination (default: 1)
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int _page { get; set; } = 1;

    /// <summary>
    /// Number of items per page (default: 10)
    /// </summary>
    [Range(1, 100, ErrorMessage = "Size must be between 1 and 100")]
    public int _size { get; set; } = 10;

    /// <summary>
    /// Ordering parameter (e.g., "username asc", "email desc")
    /// </summary>
    public string? _order { get; set; }

    // Basic Filters - exact match and wildcard support
    /// <summary>
    /// Filter by username (supports wildcards)
    /// </summary>
    public string? username { get; set; }

    /// <summary>
    /// Filter by email (supports wildcards)
    /// </summary>
    public string? email { get; set; }

    /// <summary>
    /// Filter by role (exact match)
    /// </summary>
    public string? role { get; set; }

    /// <summary>
    /// Filter by status (exact match)
    /// </summary>
    public string? status { get; set; }

    /// <summary>
    /// Filter by first name (supports wildcards)
    /// </summary>
    public string? firstName { get; set; }

    /// <summary>
    /// Filter by last name (supports wildcards)
    /// </summary>
    public string? lastName { get; set; }

    /// <summary>
    /// Filter by city (supports wildcards)
    /// </summary>
    public string? city { get; set; }

    /// <summary>
    /// Filter by street (supports wildcards)
    /// </summary>
    public string? street { get; set; }

    /// <summary>
    /// Filter by phone (supports wildcards)
    /// </summary>
    public string? phone { get; set; }

    // Properties for backward compatibility and mapping
    public int Page => _page;
    public int Size => _size;
    public string? Order => _order;
} 