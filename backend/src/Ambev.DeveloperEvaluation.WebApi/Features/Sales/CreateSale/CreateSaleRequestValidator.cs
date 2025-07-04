using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerId: Must be a non-empty GUID
    /// - BranchId: Must be a non-empty GUID
    /// - Items: Must contain at least one valid sale item
    /// </remarks>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(sale => sale.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId is required.");

            RuleFor(sale => sale.BranchId)
                .NotEmpty()
                .WithMessage("BranchId is required.");

            RuleFor(sale => sale.Items)
                .NotNull()
                .WithMessage("Items cannot be null.")
                .Must(items => items.Count > 0)
                .WithMessage("At least one item is required.")
                .ForEach(item =>
                {
                    item.SetValidator(new CreateSaleItemRequestValidator());
                });
        }
    }
}
