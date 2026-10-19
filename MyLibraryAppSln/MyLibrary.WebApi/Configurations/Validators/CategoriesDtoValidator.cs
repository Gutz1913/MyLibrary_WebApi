using FluentValidation;
using MyLibrary.WebApi.DTOs.Request;

namespace MyLibrary.WebApi.Configurations.Validators;

public class CategoriesDtoValidator : AbstractValidator<CategoryRequestDTO>
{
    public CategoriesDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().MaximumLength(50);
        RuleFor(dto => dto.Description).NotEmpty().MaximumLength(int.MaxValue);
    }
}
