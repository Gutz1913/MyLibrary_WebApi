using FluentValidation;
using MyLibrary.WebApi.DTOs.Request;

namespace MyLibrary.WebApi.Configurations.Validators;

public class BooksDtoValidator : AbstractValidator<BookRequestDTO>
{
    public BooksDtoValidator()
    {
        RuleFor(dto => dto.Isbn).NotEmpty().MaximumLength(100);
        RuleFor(dto => dto.Title).NotEmpty().MaximumLength(100);
        RuleFor(dto => dto.Subtitle).MaximumLength(100);
        RuleFor(dto => dto.Language).NotEmpty().MaximumLength(50);
        RuleFor(dto => dto.Synopsis).MaximumLength(int.MaxValue);
        RuleFor(dto => dto.PublisherId).NotEmpty().GreaterThan(0);
    }
}
