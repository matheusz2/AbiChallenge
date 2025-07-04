using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must be a non-empty GUID
    /// - Quantity: Must be greater than zero
    /// - UnitPrice: Must be greater than zero
    /// </remarks>
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0)
                .WithMessage("UnitPrice must be greater than zero.");
        }
    }
}
