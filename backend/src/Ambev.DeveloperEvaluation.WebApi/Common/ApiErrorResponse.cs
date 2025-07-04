namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Standard error response format as specified in general-api.md
/// </summary>
public class ApiErrorResponse
{
    /// <summary>
    /// A machine-readable error type identifier
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// A short, human-readable summary of the problem
    /// </summary>
    public string Error { get; set; } = string.Empty;

    /// <summary>
    /// A human-readable explanation specific to this occurrence of the problem
    /// </summary>
    public string Detail { get; set; } = string.Empty;

    /// <summary>
    /// Creates a new ApiErrorResponse
    /// </summary>
    /// <param name="type">Error type identifier</param>
    /// <param name="error">Short error summary</param>
    /// <param name="detail">Detailed error explanation</param>
    public ApiErrorResponse(string type, string error, string detail)
    {
        Type = type;
        Error = error;
        Detail = detail;
    }

    /// <summary>
    /// Creates a ResourceNotFound error response
    /// </summary>
    /// <param name="resource">The resource that was not found</param>
    /// <param name="identifier">The identifier used to search for the resource</param>
    /// <returns>ApiErrorResponse for resource not found</returns>
    public static ApiErrorResponse ResourceNotFound(string resource, string identifier)
    {
        return new ApiErrorResponse(
            "ResourceNotFound",
            $"{resource} not found",
            $"The {resource.ToLower()} with ID {identifier} does not exist in our database"
        );
    }

    /// <summary>
    /// Creates a ValidationError response
    /// </summary>
    /// <param name="detail">Detailed validation error message</param>
    /// <returns>ApiErrorResponse for validation error</returns>
    public static ApiErrorResponse ValidationError(string detail)
    {
        return new ApiErrorResponse(
            "ValidationError",
            "Invalid input data",
            detail
        );
    }

    /// <summary>
    /// Creates an AuthenticationError response
    /// </summary>
    /// <param name="detail">Detailed authentication error message</param>
    /// <returns>ApiErrorResponse for authentication error</returns>
    public static ApiErrorResponse AuthenticationError(string detail)
    {
        return new ApiErrorResponse(
            "AuthenticationError",
            "Invalid authentication token",
            detail
        );
    }
} 