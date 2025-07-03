using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Validator for UpdateUserRequest
/// </summary>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateUserRequest
    /// </summary>
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .Length(3, 50)
            .WithMessage("Username must be between 3 and 50 characters");

        RuleFor(x => x.Name.Firstname)
            .NotEmpty()
            .WithMessage("First name is required")
            .Length(1, 50)
            .WithMessage("First name must be between 1 and 50 characters");

        RuleFor(x => x.Name.Lastname)
            .NotEmpty()
            .WithMessage("Last name is required")
            .Length(1, 50)
            .WithMessage("Last name must be between 1 and 50 characters");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone is required")
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
            .WithMessage("Phone must be in format (XX) XXXXX-XXXX");

        RuleFor(x => x.Address.City)
            .NotEmpty()
            .WithMessage("City is required")
            .Length(1, 100)
            .WithMessage("City must be between 1 and 100 characters");

        RuleFor(x => x.Address.Street)
            .NotEmpty()
            .WithMessage("Street is required")
            .Length(1, 200)
            .WithMessage("Street must be between 1 and 200 characters");

        RuleFor(x => x.Address.Number)
            .GreaterThan(0)
            .WithMessage("Number must be greater than 0");

        RuleFor(x => x.Address.Zipcode)
            .NotEmpty()
            .WithMessage("Zipcode is required")
            .Length(5, 20)
            .WithMessage("Zipcode must be between 5 and 20 characters");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid status value");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Invalid role value");

        // Password validation only if provided
        When(x => !string.IsNullOrWhiteSpace(x.Password), () => {
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long")
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"\d")
                .WithMessage("Password must contain at least one number")
                .Matches(@"[\W_]")
                .WithMessage("Password must contain at least one special character");
        });
    }
} 