using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand that defines validation rules for sale update command.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must be a non-empty GUID
    /// - CustomerId: Must be a non-empty GUID
    /// - BranchId: Must be a non-empty GUID
    /// - Items: Must not be empty and must contain valid items
    /// - Items count: Maximum 20 items allowed
    /// - Each item: Must have valid ProductId, Quantity > 0, and UnitPrice > 0
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must contain at least one item");

        RuleFor(sale => sale.Items.Count)
            .LessThanOrEqualTo(20)
            .WithMessage("Sale cannot contain more than 20 items");

        RuleForEach(sale => sale.Items)
            .SetValidator(new UpdateSaleItemCommandValidator());
    }
}

/// <summary>
/// Validator for UpdateSaleItemCommand
/// </summary>
public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
{
    /// <summary>
    /// Initializes validation rules for UpdateSaleItemCommand
    /// </summary>
    public UpdateSaleItemCommandValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");
    }
} 