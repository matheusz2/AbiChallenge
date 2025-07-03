using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for SaleItem entity that defines validation rules for sale item business logic.
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    /// <summary>
    /// Initializes a new instance of the SaleItemValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must be a non-empty GUID
    /// - Quantity: Must be greater than zero
    /// - UnitPrice: Must be greater than zero
    /// </remarks>
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero.");
    }
} 