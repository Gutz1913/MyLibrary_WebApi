using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IValidator<ReservationRequestDTO> _validator;

    public ReservationsController(IReservationService reservationService, IValidator<ReservationRequestDTO> validator)
    {
        _reservationService = reservationService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reservations = await _reservationService.GetAll();

        return Ok(reservations);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reservation = await _reservationService.GetById(id);

        return Ok(reservation);
    }

    [HttpGet("get-reservations-by-bookid/{bookId}")]
    public async Task<IActionResult> GetReservationsByBookId(int bookId)
    {
        var reservations = await _reservationService.GetReservationsByBookId(bookId);

        return Ok(reservations);
    }

    [HttpGet("get-reservations-by-userid/{userId}")]
    public async Task<IActionResult> GetReservationsByUserId(int userId)
    {
        var reservations = await _reservationService.GetReservationsByUserId(userId);

        return Ok(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReservationRequestDTO reservationDto)
    {
        var validationResult = await _validator.ValidateAsync(reservationDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var reservation = await _reservationService.Add(reservationDto);

        return Ok(reservation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ReservationRequestDTO reservationDto)
    {
        var reservation = await _reservationService.Update(id, reservationDto);
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reservation = await _reservationService.Delete(id);
        if (reservation == null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }
}
