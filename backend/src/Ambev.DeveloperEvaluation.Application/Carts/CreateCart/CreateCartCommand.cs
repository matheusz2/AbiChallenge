using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Command for creating a new cart
/// </summary>
public class CreateCartCommand : IRequest<CreateCartResult>
{
    /// <summary>
    /// Gets or sets the user ID associated with this cart
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the cart was created
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the collection of products in this cart
    /// </summary>
    public List<CreateCartProductCommand> Products { get; set; } = new();
}

/// <summary>
/// Represents a product within a cart creation command
/// </summary>
public class CreateCartProductCommand
{
    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of this product in the cart
    /// </summary>
    public int Quantity { get; set; }
}

/// <summary>
/// Response model for CreateCart operation
/// </summary>
public class CreateCartResult
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
    public List<CreateCartProductResult> Products { get; set; } = new();
}

/// <summary>
/// Response model for cart product
/// </summary>
public class CreateCartProductResult
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