using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their username
    /// </summary>
    /// <param name="username">The username to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o => o.Username == username, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email
    /// </summary>
    /// <param name="email">The email to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o => o.Email == email, cancellationToken);
    }

    /// <summary>
    /// Retrieves all users with pagination, ordering and filtering
    /// </summary>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="orderBy">Ordering parameter</param>
    /// <param name="filter">Filter criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of users</returns>
    public async Task<List<User>> GetAllAsync(int page, int pageSize, string? orderBy = null, UserFilter? filter = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsQueryable();
        
        // Aplicar filtros
        query = ApplyFilters(query, filter);
        
        // Aplicar ordenação
        query = ApplyOrdering(query, orderBy);
        
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="user">The user to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user</returns>
    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Deletes a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Applies filtering to the query based on the filter criteria
    /// </summary>
    /// <param name="query">The query to apply filters to</param>
    /// <param name="filter">The filter criteria</param>
    /// <returns>The filtered query</returns>
    private static IQueryable<User> ApplyFilters(IQueryable<User> query, UserFilter? filter)
    {
        if (filter == null) return query;

        // Username filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.Username))
        {
            if (filter.HasUsernameWildcard)
            {
                var usernamePattern = filter.GetUsernameWithoutWildcard();
                if (filter.Username.StartsWith("*") && filter.Username.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(u => u.Username.ToLower().Contains(usernamePattern.ToLower()));
                }
                else if (filter.Username.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(u => u.Username.ToLower().EndsWith(usernamePattern.ToLower()));
                }
                else if (filter.Username.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(u => u.Username.ToLower().StartsWith(usernamePattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(u => u.Username.ToLower() == filter.Username.ToLower());
            }
        }

        // Email filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.Email))
        {
            if (filter.HasEmailWildcard)
            {
                var emailPattern = filter.GetEmailWithoutWildcard();
                if (filter.Email.StartsWith("*") && filter.Email.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(u => u.Email.ToLower().Contains(emailPattern.ToLower()));
                }
                else if (filter.Email.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(u => u.Email.ToLower().EndsWith(emailPattern.ToLower()));
                }
                else if (filter.Email.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(u => u.Email.ToLower().StartsWith(emailPattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(u => u.Email.ToLower() == filter.Email.ToLower());
            }
        }

        // Role filter (exact match)
        if (!string.IsNullOrEmpty(filter.Role))
        {
            query = query.Where(u => u.Role.ToString().ToLower() == filter.Role.ToLower());
        }

        // Status filter (exact match)
        if (!string.IsNullOrEmpty(filter.Status))
        {
            query = query.Where(u => u.Status.ToString().ToLower() == filter.Status.ToLower());
        }

        // First name filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            if (filter.HasFirstNameWildcard)
            {
                var firstNamePattern = filter.GetFirstNameWithoutWildcard();
                if (filter.FirstName.StartsWith("*") && filter.FirstName.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(u => u.Name.Firstname.ToLower().Contains(firstNamePattern.ToLower()));
                }
                else if (filter.FirstName.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(u => u.Name.Firstname.ToLower().EndsWith(firstNamePattern.ToLower()));
                }
                else if (filter.FirstName.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(u => u.Name.Firstname.ToLower().StartsWith(firstNamePattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(u => u.Name.Firstname.ToLower() == filter.FirstName.ToLower());
            }
        }

        // Last name filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.LastName))
        {
            if (filter.HasLastNameWildcard)
            {
                var lastNamePattern = filter.GetLastNameWithoutWildcard();
                if (filter.LastName.StartsWith("*") && filter.LastName.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(u => u.Name.Lastname.ToLower().Contains(lastNamePattern.ToLower()));
                }
                else if (filter.LastName.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(u => u.Name.Lastname.ToLower().EndsWith(lastNamePattern.ToLower()));
                }
                else if (filter.LastName.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(u => u.Name.Lastname.ToLower().StartsWith(lastNamePattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(u => u.Name.Lastname.ToLower() == filter.LastName.ToLower());
            }
        }

        // City filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.City))
        {
            if (filter.HasCityWildcard)
            {
                var cityPattern = filter.GetCityWithoutWildcard();
                if (filter.City.StartsWith("*") && filter.City.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(u => u.Address.City.ToLower().Contains(cityPattern.ToLower()));
                }
                else if (filter.City.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(u => u.Address.City.ToLower().EndsWith(cityPattern.ToLower()));
                }
                else if (filter.City.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(u => u.Address.City.ToLower().StartsWith(cityPattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(u => u.Address.City.ToLower() == filter.City.ToLower());
            }
        }

        // Street filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.Street))
        {
            if (filter.HasStreetWildcard)
            {
                var streetPattern = filter.GetStreetWithoutWildcard();
                if (filter.Street.StartsWith("*") && filter.Street.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(u => u.Address.Street.ToLower().Contains(streetPattern.ToLower()));
                }
                else if (filter.Street.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(u => u.Address.Street.ToLower().EndsWith(streetPattern.ToLower()));
                }
                else if (filter.Street.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(u => u.Address.Street.ToLower().StartsWith(streetPattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(u => u.Address.Street.ToLower() == filter.Street.ToLower());
            }
        }

        // Phone filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.Phone))
        {
            if (filter.HasPhoneWildcard)
            {
                var phonePattern = filter.GetPhoneWithoutWildcard();
                if (filter.Phone.StartsWith("*") && filter.Phone.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(u => u.Phone.ToLower().Contains(phonePattern.ToLower()));
                }
                else if (filter.Phone.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(u => u.Phone.ToLower().EndsWith(phonePattern.ToLower()));
                }
                else if (filter.Phone.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(u => u.Phone.ToLower().StartsWith(phonePattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(u => u.Phone.ToLower() == filter.Phone.ToLower());
            }
        }

        return query;
    }

    /// <summary>
    /// Applies ordering to the query based on the orderBy parameter
    /// </summary>
    /// <param name="query">The query to apply ordering to</param>
    /// <param name="orderBy">The ordering parameter</param>
    /// <returns>The ordered query</returns>
    private static IQueryable<User> ApplyOrdering(IQueryable<User> query, string? orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return query.OrderBy(u => u.Username);
        }

        return orderBy.ToLower() switch
        {
            "username" or "username_asc" => query.OrderBy(u => u.Username),
            "username_desc" => query.OrderByDescending(u => u.Username),
            "email" or "email_asc" => query.OrderBy(u => u.Email),
            "email_desc" => query.OrderByDescending(u => u.Email),
            "role" or "role_asc" => query.OrderBy(u => u.Role),
            "role_desc" => query.OrderByDescending(u => u.Role),
            "status" or "status_asc" => query.OrderBy(u => u.Status),
            "status_desc" => query.OrderByDescending(u => u.Status),
            "firstname" or "firstname_asc" => query.OrderBy(u => u.Name.Firstname),
            "firstname_desc" => query.OrderByDescending(u => u.Name.Firstname),
            "lastname" or "lastname_asc" => query.OrderBy(u => u.Name.Lastname),
            "lastname_desc" => query.OrderByDescending(u => u.Name.Lastname),
            "id" or "id_asc" => query.OrderBy(u => u.Id),
            "id_desc" => query.OrderByDescending(u => u.Id),
            _ => query.OrderBy(u => u.Username)
        };
    }
}
