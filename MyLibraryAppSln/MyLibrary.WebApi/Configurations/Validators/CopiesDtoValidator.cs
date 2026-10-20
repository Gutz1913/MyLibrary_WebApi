using FluentValidation;
using MyLibrary.WebApi.DTOs.Request;

namespace MyLibrary.WebApi.Configurations.Validators;

public class CopiesDtoValidator : AbstractValidator<CopyRequestDTO>
{
    public CopiesDtoValidator()
    {
        RuleFor(dto => dto.Barcode).NotEmpty().MaximumLength(50);
        RuleFor(dto => dto.Location).MaximumLength(100);
        RuleFor(dto => dto.Condition).MaximumLength(50);
        RuleFor(dto => dto.BookId).NotEmpty().GreaterThan(0);
    }
}
