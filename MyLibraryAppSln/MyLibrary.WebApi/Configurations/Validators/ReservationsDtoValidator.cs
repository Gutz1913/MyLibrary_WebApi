using FluentValidation;
using MyLibrary.WebApi.DTOs.Request;

namespace MyLibrary.WebApi.Configurations.Validators;

public class ReservationsDtoValidator : AbstractValidator<ReservationRequestDTO>
{
    public ReservationsDtoValidator()
    {
        RuleFor(dto => dto.BookId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.UserId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.ReservationDate).NotEmpty();
        RuleFor(dto => dto.ExpirationDate).NotEmpty();
        RuleFor(dto => dto.State).NotEmpty().MaximumLength(50);
    }
}
