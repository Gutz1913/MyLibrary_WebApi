using FluentValidation;
using MyLibrary.WebApi.DTOs.Request;

namespace MyLibrary.WebApi.Configurations.Validators; 

public class AuthorsDtoValidator : AbstractValidator<AuthorRequestDTO>
{
    public AuthorsDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().MaximumLength(50);
        RuleFor(dto => dto.Nationality).NotEmpty().MaximumLength(50);
        RuleFor(dto => dto.BirthDate).NotEmpty();
        RuleFor(dto => dto.Biography).MaximumLength(int.MaxValue);
    }
}
