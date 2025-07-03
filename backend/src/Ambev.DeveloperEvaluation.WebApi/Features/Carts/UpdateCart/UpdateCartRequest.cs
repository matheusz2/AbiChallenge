using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Request model for updating a cart
/// </summary>
public class UpdateCartRequest
{
    /// <summary>
    /// The unique identifier of the cart to update
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
    public List<UpdateCartProductRequest> Products { get; set; } = new();
}

/// <summary>
/// Request model for a product within a cart update
/// </summary>
public class UpdateCartProductRequest
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