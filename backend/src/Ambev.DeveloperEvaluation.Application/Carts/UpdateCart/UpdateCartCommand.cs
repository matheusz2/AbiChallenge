using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating an existing cart
/// </summary>
public class UpdateCartCommand : IRequest<UpdateCartResult>
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
    public List<UpdateCartProductCommand> Products { get; set; } = new();
}

/// <summary>
/// Represents a product within a cart update command
/// </summary>
public class UpdateCartProductCommand
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

/// <summary>
/// Response model for UpdateCart operation
/// </summary>
public class UpdateCartResult
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
    public List<UpdateCartProductResult> Products { get; set; } = new();
}

/// <summary>
/// Response model for cart product in update operation
/// </summary>
public class UpdateCartProductResult
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