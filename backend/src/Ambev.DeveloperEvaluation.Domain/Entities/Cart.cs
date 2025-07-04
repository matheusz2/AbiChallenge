using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a shopping cart in the system
/// </summary>
public class Cart : BaseEntity
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
    public List<CartProduct> Products { get; set; } = new();

    /// <summary>
    /// Gets the date and time when the cart was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the cart
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Cart class
    /// </summary>
    public Cart()
    {
        CreatedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Represents a product within a cart
/// </summary>
public class CartProduct
{
    /// <summary>
    /// Gets or sets the cart ID this product belongs to
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of this product in the cart
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Navigation property to the cart
    /// </summary>
    public Cart Cart { get; set; } = null!;
} 