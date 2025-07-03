using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user in the repository
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their username
    /// </summary>
    /// <param name="username">The username to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their email
    /// </summary>
    /// <param name="email">The email to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all users with pagination, ordering and filtering
    /// </summary>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="orderBy">Ordering parameter</param>
    /// <param name="filter">Filter criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of users</returns>
    Task<List<User>> GetAllAsync(int page, int pageSize, string? orderBy = null, UserFilter? filter = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="user">The user to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user</returns>
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
