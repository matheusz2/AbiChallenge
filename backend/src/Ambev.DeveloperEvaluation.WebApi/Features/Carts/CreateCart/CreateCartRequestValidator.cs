using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Validator for CreateCartRequest
/// </summary>
public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{
    /// <summary>
    /// Initializes validation rules for CreateCartRequest
    /// </summary>
    public CreateCartRequestValidator()
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
            .SetValidator(new CreateCartProductRequestValidator());
    }
}

/// <summary>
/// Validator for CreateCartProductRequest
/// </summary>
public class CreateCartProductRequestValidator : AbstractValidator<CreateCartProductRequest>
{
    /// <summary>
    /// Initializes validation rules for CreateCartProductRequest
    /// </summary>
    public CreateCartProductRequestValidator()
    {
        RuleFor(product => product.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than 0");

        RuleFor(product => product.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
} 