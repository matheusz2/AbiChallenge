using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Response model for creating a cart
/// </summary>
public class CreateCartResponse
{
    /// <summary>
    /// The unique identifier of the created cart
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
    public List<CreateCartProductResponse> Products { get; set; } = new();
}

/// <summary>
/// Response model for a product within a created cart
/// </summary>
public class CreateCartProductResponse
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