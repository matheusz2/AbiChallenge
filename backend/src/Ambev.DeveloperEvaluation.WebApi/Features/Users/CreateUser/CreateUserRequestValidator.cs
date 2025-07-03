using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Validator for CreateUserRequest that validates user creation data according to business rules.
/// </summary>
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of CreateUserRequestValidator with validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Username: Required, must be 3-50 characters
    /// - Password: Required, must be 8-100 characters with specific complexity requirements
    /// - Email: Required, must be valid email format
    /// - Phone: Must be valid phone format
    /// - Status: Must be valid UserStatus enum value
    /// - Role: Must be valid UserRole enum value
    /// </remarks>
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .Length(3, 50)
            .WithMessage("Username must be between 3 and 50 characters");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long")
            .MaximumLength(100)
            .WithMessage("Password must not exceed 100 characters")
            .Matches(@"[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]")
            .WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d")
            .WithMessage("Password must contain at least one number")
            .Matches(@"[\W_]")
            .WithMessage("Password must contain at least one special character");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .MaximumLength(100)
            .WithMessage("Email must not exceed 100 characters");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone is required")
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
            .WithMessage("Phone must be in format (XX) XXXXX-XXXX");

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
            .WithMessage("Status must be a valid value");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Role must be a valid value");
    }
}