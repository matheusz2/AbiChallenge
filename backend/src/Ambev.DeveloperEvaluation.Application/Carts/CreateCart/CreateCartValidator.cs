using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Validator for CreateCartCommand
/// </summary>
public class CreateCartValidator : AbstractValidator<CreateCartCommand>
{
    /// <summary>
    /// Initializes validation rules for CreateCartCommand
    /// </summary>
    public CreateCartValidator()
    {
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
            .SetValidator(new CreateCartProductValidator());
    }
}

/// <summary>
/// Validator for CreateCartProductCommand
/// </summary>
public class CreateCartProductValidator : AbstractValidator<CreateCartProductCommand>
{
    /// <summary>
    /// Initializes validation rules for CreateCartProductCommand
    /// </summary>
    public CreateCartProductValidator()
    {
        RuleFor(product => product.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than 0");

        RuleFor(product => product.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
} 