using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Command for retrieving all carts with pagination
/// </summary>
public class GetAllCartsCommand : IRequest<GetAllCartsResult>
{
    /// <summary>
    /// Page number (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Ordering parameter
    /// </summary>
    public string? OrderBy { get; set; }
}

/// <summary>
/// Response model for GetAllCarts operation
/// </summary>
public class GetAllCartsResult
{
    /// <summary>
    /// The list of carts
    /// </summary>
    public List<GetAllCartsItemResult> Data { get; set; } = new();

    /// <summary>
    /// Current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of carts
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// Individual cart item in the GetAllCarts response
/// </summary>
public class GetAllCartsItemResult
{
    /// <summary>
    /// The unique identifier of the cart
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user ID associated with this cart
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The date when the cart was created
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The collection of products in this cart
    /// </summary>
    public List<GetAllCartsProductResult> Products { get; set; } = new();
}

/// <summary>
/// Product item in the cart
/// </summary>
public class GetAllCartsProductResult
{
    /// <summary>
    /// The product ID
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// The quantity of this product in the cart
    /// </summary>
    public int Quantity { get; set; }
} 