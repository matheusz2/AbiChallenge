using MediatR;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Command for retrieving all users with pagination, ordering and filtering
/// </summary>
public class GetAllUsersCommand : IRequest<GetAllUsersResult>
{
    /// <summary>
    /// Page number for pagination (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Optional ordering parameter (e.g., "username asc", "email desc")
    /// </summary>
    public string? Order { get; set; }

    /// <summary>
    /// Filter criteria for users
    /// </summary>
    public UserFilter? Filter { get; set; }
} 