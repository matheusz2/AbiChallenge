using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

/// <summary>
/// Response model for retrieving all carts
/// </summary>
public class GetAllCartsResponse
{
    /// <summary>
    /// The list of carts
    /// </summary>
    public List<GetAllCartsItemResponse> Data { get; set; } = new();

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
/// Individual cart item in the response
/// </summary>
public class GetAllCartsItemResponse
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
    public List<GetAllCartsProductResponse> Products { get; set; } = new();
}

/// <summary>
/// Product item in the cart
/// </summary>
public class GetAllCartsProductResponse
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