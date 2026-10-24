using FluentValidation;
using MyLibrary.WebApi.DTOs.Request;

namespace MyLibrary.WebApi.Configurations.Validators;

public class FinesDtoValidator : AbstractValidator<FineRequestDTO>
{
    public FinesDtoValidator()
    {
        RuleFor(dto => dto.LoanId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.UserId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.Reason).MaximumLength(100);
        RuleFor(dto => dto.Amount).NotEmpty().PrecisionScale(18,2,true);
        RuleFor(dto => dto.State).NotEmpty().MaximumLength(20);

    }
}
