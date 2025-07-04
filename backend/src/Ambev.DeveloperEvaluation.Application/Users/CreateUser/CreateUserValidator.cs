using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Username: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// - Firstname: Required, must be between 1 and 50 characters
    /// - Lastname: Required, must be between 1 and 50 characters
    /// - City: Required, must be between 1 and 100 characters
    /// - Street: Required, must be between 1 and 200 characters
    /// - Number: Must be greater than 0
    /// - Zipcode: Required, must be between 5 and 20 characters
    /// </remarks>
    public CreateUserValidator()
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

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
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

        RuleFor(x => x.Firstname)
            .NotEmpty()
            .WithMessage("First name is required")
            .Length(1, 50)
            .WithMessage("First name must be between 1 and 50 characters");

        RuleFor(x => x.Lastname)
            .NotEmpty()
            .WithMessage("Last name is required")
            .Length(1, 50)
            .WithMessage("Last name must be between 1 and 50 characters");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone is required")
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
            .WithMessage("Phone must be in format (XX) XXXXX-XXXX");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required")
            .Length(1, 100)
            .WithMessage("City must be between 1 and 100 characters");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street is required")
            .Length(1, 200)
            .WithMessage("Street must be between 1 and 200 characters");

        RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("Number must be greater than 0");

        RuleFor(x => x.Zipcode)
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
    }
}