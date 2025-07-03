using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Command for deleting a cart
/// </summary>
public class DeleteCartCommand : IRequest<DeleteCartResult>
{
    /// <summary>
    /// The unique identifier of the cart to delete
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of DeleteCartCommand
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    public DeleteCartCommand(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// Response model for DeleteCart operation
/// </summary>
public class DeleteCartResult
{
    /// <summary>
    /// Indicates whether the deletion was successful
    /// </summary>
    public bool Success { get; set; }
} 