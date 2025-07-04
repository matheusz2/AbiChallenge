using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartCommand
/// </summary>
public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    /// <summary>
    /// Initializes validation rules for UpdateCartCommand
    /// </summary>
    public UpdateCartValidator()
    {
        RuleFor(cart => cart.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required");

        RuleFor(cart => cart.UserId)
            .GreaterThan(0)
            .WithMessage("User ID must be greater than 0");

        RuleFor(cart => cart.Date)
            .NotEmpty()
            .WithMessage("Date is required");

        RuleFor(cart => cart.Products)
            .NotNull()
            .WithMessage("Products list cannot be null")
            .Must(products => products.Count > 0)
            .WithMessage("Cart must contain at least one product");

        RuleForEach(cart => cart.Products)
            .SetValidator(new UpdateCartProductValidator());
    }
}

/// <summary>
/// Validator for UpdateCartProductCommand
/// </summary>
public class UpdateCartProductValidator : AbstractValidator<UpdateCartProductCommand>
{
    /// <summary>
    /// Initializes validation rules for UpdateCartProductCommand
    /// </summary>
    public UpdateCartProductValidator()
    {
        RuleFor(product => product.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than 0");

        RuleFor(product => product.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
} 