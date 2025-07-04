using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartRequest
/// </summary>
public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateCartRequest
    /// </summary>
    public UpdateCartRequestValidator()
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
            .SetValidator(new UpdateCartProductRequestValidator());
    }
}

/// <summary>
/// Validator for UpdateCartProductRequest
/// </summary>
public class UpdateCartProductRequestValidator : AbstractValidator<UpdateCartProductRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateCartProductRequest
    /// </summary>
    public UpdateCartProductRequestValidator()
    {
        RuleFor(product => product.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than 0");

        RuleFor(product => product.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
} 