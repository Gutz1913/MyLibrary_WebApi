using FluentValidation;
using MyLibrary.WebApi.DTOs.Request;

namespace MyLibrary.WebApi.Configurations.Validators;

public class LoansDtoValidator : AbstractValidator<LoanRequestDTO>
{
    public LoansDtoValidator()
    {
        RuleFor(dto => dto.CopyId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.UserId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.State).NotEmpty().MaximumLength(20);
    }
}
