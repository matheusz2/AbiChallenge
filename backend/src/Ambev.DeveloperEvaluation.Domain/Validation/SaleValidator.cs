using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for Sale entity that defines validation rules for sale business logic.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    /// <summary>
    /// Initializes a new instance of the SaleValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerId: Must be a non-empty GUID
    /// - BranchId: Must be a non-empty GUID
    /// - Items: Must not be empty and must contain valid items
    /// - Items count: Maximum 20 items allowed
    /// - Each item: Must be validated using SaleItemValidator
    /// </remarks>
    public SaleValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must contain at least one item.");

        RuleFor(sale => sale.Items.Count)
            .LessThanOrEqualTo(20)
            .WithMessage("Sale cannot contain more than 20 items.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
} 